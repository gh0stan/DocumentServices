using InvoiceXml.Infrastructure.Persistence;
using InvoiceXml.Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using InvoiceXml.Application;
using InvoiceXml.Infrastructure;
using System.Reflection;
using InvoiceXml.Application.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Host.ConfigureAppConfiguration((hostingContext, configBuilder) =>
{
    // Take keys from DB. Service requires restart on fresh deploy to get settings from migration.
    var configSource = new EFConfigurationSource(opts =>
        opts.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceXml")));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
