using Dapr.Client;
using Dapr.Extensions.Configuration;
using WisdomPetMedicine.Rescue.Api.ApplicationServices;
using WisdomPetMedicine.Rescue.Api.Extensions;
using WisdomPetMedicine.Rescue.Api.Infrastructure;
using WisdomPetMedicine.Rescue.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureAppConfiguration(config =>
{
    var daprClient = new DaprClientBuilder().Build();
    config.AddDaprSecretStore("wisdomsecretstore", daprClient);
});

// Add services to the container.
builder.Services.AddRescueDb(builder.Configuration);
builder.Services.AddScoped<AdopterApplicationService>();
builder.Services.AddScoped<IRescueRepository, RescueRepository>();
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

app.EnsureRescueDbIsCreated();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCloudEvents();
app.MapSubscribeHandler();
app.MapControllers();

app.Run();