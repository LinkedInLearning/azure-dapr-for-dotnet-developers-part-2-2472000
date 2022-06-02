using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using WisdomPetMedicine.PetAggregator.Api.Models;

namespace WisdomPetMedicine.PetAggregator.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class PetAggregatorController : ControllerBase
{
    private readonly DaprClient daprClient;
    private readonly ILogger<PetAggregatorController> logger;

    public PetAggregatorController(DaprClient daprClient, ILogger<PetAggregatorController> logger)
    {
        this.daprClient = daprClient;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lastQuery = await daprClient.GetStateEntryAsync<StateModel>("statestore", "lastquery");
        int lastQueryDurationInSeconds = await GetConfiguredLastQueryDurationInSecondsAsync();

        logger.LogInformation($"LastQueryDurationInSeconds is: {lastQueryDurationInSeconds}");

        if (lastQuery.Value != null && DateTime.UtcNow <= lastQuery.Value.LastQuery.AddSeconds(lastQueryDurationInSeconds))
        {
            return Ok(lastQuery.Value.Data);
        }

        IEnumerable<dynamic>? result = null;

        bool saved = false;
        while (!saved)
        {
            result = await QueryPets();
            lastQuery.Value = new StateModel(DateTime.UtcNow, result);
            saved = await lastQuery.TrySaveAsync();
        }

        return Ok(result);
    }

    private async Task<int> GetConfiguredLastQueryDurationInSecondsAsync()
    {
        try
        {
            var configuration = await daprClient.GetConfiguration("wisdomconfigstore", new List<string>() { "LastQueryDurationInSeconds" });
            _ = int.TryParse(configuration.Items[0].Value, out int lastQueryDurationInSeconds);
            return lastQueryDurationInSeconds;
        }
        catch (Exception)
        {
            return 30;
        }
    }

    private async Task<IEnumerable<dynamic>> QueryPets()
    {
        var pets = await daprClient.InvokeMethodAsync<IEnumerable<PetModel>>(HttpMethod.Get, "pet", "petquery");

        var rescues = await daprClient.InvokeMethodAsync<IEnumerable<RescueModel>>(HttpMethod.Get, "rescuequery", "rescuequery");

        var patients = await daprClient.InvokeMethodAsync<IEnumerable<PatientModel>>(HttpMethod.Get, "hospital", "patientquery");

        var result = from pet in pets
                     join patient in patients on pet.Id equals patient.Id
                     join rescue in rescues on pet.Id equals rescue.Id
                     select new
                     {
                         pet.Id,
                         pet.Name,
                         pet.Breed,
                         pet.Sex,
                         pet.Color,
                         pet.DateOfBirth,
                         pet.Species,
                         Hospital = new
                         {
                             patient.BloodType,
                             patient.Weight,
                             patient.Status,
                         },
                         Rescue = new
                         {
                             rescue.AdopterId,
                             rescue.AdopterName,
                             rescue.AdoptionStatus
                         }
                     };
        return result;
    }
}