using Dapr.Client;
using Dapr.Extensions.Configuration;
using WisdomPetMedicine.Hospital.Api.ApplicationServices;
using WisdomPetMedicine.Hospital.Api.Extensions;
using WisdomPetMedicine.Hospital.Domain.Repositories;
using WisdomPetMedicine.Hospital.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureAppConfiguration(config =>
{
    var daprClient = new DaprClientBuilder().Build();
    config.AddDaprSecretStore("wisdomsecretstore", daprClient);
});

// Add services to the container.
builder.Services.AddHospitalDb(builder.Configuration);
builder.Services.AddSingleton<IPatientAggregateStore, PatientAggregateStore>();
builder.Services.AddScoped<HospitalApplicationService>();
builder.Services.AddControllers()
                .AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.EnsureHospitalDbIsCreated();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCloudEvents();
app.MapSubscribeHandler();
app.MapControllers();
app.Run();