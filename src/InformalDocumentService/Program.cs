using InformalDocumentService.Data;
using Microsoft.EntityFrameworkCore;
using InformalDocumentService.Data.Repositories;
using InformalDocumentService.Data.EntityConfiguration;
using InformalDocumentService.EventProcessing;
using InformalDocumentService.AsyncDataServices;
using InformalDocumentService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("InformalConn")));

builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddScoped<IDocumentRepo, DocumentRepo>();
builder.Services.AddScoped<IAbonentDataClient, AbonentDataClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.ConfigureAppConfiguration((hostingContext, configBuilder) =>
{
    // Take keys from DB. Service requires restart on fresh deploy to get settings from migration.  
    var configSource = new EFConfigurationSource(opts =>
        opts.UseSqlServer(builder.Configuration.GetConnectionString("InformalConn")));
    try
    {
        configBuilder.Add(configSource);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR. Could not get configuration settings from DB. {ex.Message}");
    }

    // Overwrite from appsettings.Development for Local Dev Environment
    if (builder.Environment.IsDevelopment() && !string.IsNullOrEmpty(builder.Environment.ApplicationName))
    {
        configBuilder.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

PrepDb.PrepPopulation(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
