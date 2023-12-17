using AbonentService.AsyncDataServices;
using AbonentService.Data;
using AbonentService.Data.EntityConfiguration;
using AbonentService.Data.Repositories;
using AbonentService.Interfaces;
using AbonentService.SyncDataServices.Grpc;
using AbonentService.SyncDataServices.Http;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("AbonentsConn")));
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddScoped<IAbonentRepo, AbonentRepo>();
builder.Services.AddScoped<IAddressRepo, AddressRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<IInformalDocDataClient, HttpInformalDocDataClient>();

builder.Host.ConfigureAppConfiguration((hostingContext, configBuilder) =>
{
    // Take keys from DB. Service requires restart on fresh deploy to get settings from migration.
    var configSource = new EFConfigurationSource(opts =>
        opts.UseSqlServer(builder.Configuration.GetConnectionString("AbonentsConn")));
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

app.MapGrpcService<GrpcAbonentService>();
app.MapGet("/protos/abonents.proto", async context => 
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/abonents.proto")); 
    });
app.MapControllers();

app.Run();
