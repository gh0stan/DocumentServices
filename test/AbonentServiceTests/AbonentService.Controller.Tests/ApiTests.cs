using System.Net;
using System.Net.Http.Json;
using AbonentService.AsyncDataServices;
using AbonentService.Api.Tests.TestDoubles;
using AbonentService.Controllers;
using AbonentService.Data;
using AbonentService.Data.Repositories;
using AbonentService.Dtos;
using AbonentService.Interfaces;
using AbonentService.Profiles;
using AbonentService.SyncDataServices.Http;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Newtonsoft.Json;
using AbonentService.Data.Models;

namespace AbonentService.Api.Tests;

public class ApiTests
{   
    private static async Task<ApiFactoryInMemory> CreateApiWithAbonentRepo<T>()
        where T : class, IAbonentRepo
    {
        await using var api = new ApiFactoryInMemory(services =>
        {
            services.RemoveAll(typeof(IAbonentRepo));
            services.TryAddScoped<IAbonentRepo, T>();
            services.TryAddScoped<IMessageBusClient, DummyMessageBusClient>();
            services.TryAddScoped<IInformalDocDataClient, DummyInformalDocDataClient>();
        });

        return api;
    }

    private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();

        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        return dbSet;
    }

    [Fact]
    public async Task GetAbonentById_should_return_404_when_abonent_not_found()
    {
        var api = await CreateApiWithAbonentRepo<StubNotFoundAbonentRepo>();
        using var client = api.CreateClient();

        var response = await client.GetAsync($"api/a/Abonents/GetAbonentById/{1}");
        
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAbonentById_should_return_500_when_exception()
    {
        var api = await CreateApiWithAbonentRepo<StubExceptionAbonentRepo>();
        using var client = api.CreateClient();

        var response = await client.GetAsync($"api/a/Abonents/GetAbonentById/{1}");

        response.Should().HaveStatusCode(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task GetAbonentById_should_return_200_when_item_found()
    {
        var api = await CreateApiWithAbonentRepo<AbonentRepo>();
        using var client = api.CreateClient();

        var response = await client.GetAsync($"api/a/Abonents/GetAbonentById/{1}");

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAllAbonents_should_return_500_when_exception()
    {
        var api = await CreateApiWithAbonentRepo<StubExceptionAbonentRepo>();
        using var client = api.CreateClient();

        var response = await client.GetAsync($"api/a/Abonents/GetAllAbonents");

        response.Should().HaveStatusCode(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task GetAllAbonents_should_return_200_and_empty_list_when_not_found()
    {
        var api = await CreateApiWithAbonentRepo<StubNotFoundAbonentRepo>();
        using var client = api.CreateClient();

        var response = await client.GetAsync($"api/a/Abonents/GetAllAbonents");

        response.Should().HaveStatusCode(HttpStatusCode.OK);
        var items = JsonConvert.DeserializeObject<List<AbonentReadDto>>(
          await response.Content.ReadAsStringAsync());
        items.Should().HaveCount(0);
    }

    [Fact]
    public async Task GetAllAbonents_should_return_200_when_items_found()
    {
        var api = await CreateApiWithAbonentRepo<AbonentRepo>();
        using var client = api.CreateClient();

        var response = await client.GetAsync($"api/a/Abonents/GetAllAbonents");

        response.Should().HaveStatusCode(HttpStatusCode.OK);
        var items = JsonConvert.DeserializeObject<List<AbonentReadDto>>(await response.Content.ReadAsStringAsync());
        items.Should().HaveCount(3);
    }

    [Fact]
    public async Task CreateAbonent_should_return_500_when_exception()
    {
        var api = await CreateApiWithAbonentRepo<StubExceptionAbonentRepo>();
        using var client = api.CreateClient();
        var content = JsonContent.Create(new AbonentCreateDto
        {
            Name = "create_Test"

        });

        var response = await client.PostAsync($"api/a/Abonents/CreateAbonent", content);

        response.Should().HaveStatusCode(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CreateAbonent_should_return_200_when_created()
    {
        var api = await CreateApiWithAbonentRepo<AbonentRepo>();
        using var client = api.CreateClient();
        var content = JsonContent.Create(new AbonentCreateDto
        {
            Name = "create_Test"

        });

        var response = await client.PostAsync($"api/a/Abonents/CreateAbonent", content);

        response.Should().HaveStatusCode(HttpStatusCode.Created);
        var createdItem = JsonConvert.DeserializeObject<AbonentReadDto>(await response.Content.ReadAsStringAsync());
        createdItem.Id.Should().NotBe(0);
        createdItem.Name.Should().Be("create_Test");
    }

    [Fact]
    public void CreateAbonent_should_send_Abonent_Created_message_once_when_created()
    {
        var mockSet = GetQueryableMockDbSet(new List<Abonent> { new Abonent { Name = "Default" } });
        var mockContext = new Mock<DataContext>();
        mockContext.Setup(m => m.Abonents).Returns(mockSet.Object);
        var abonentRepo = new AbonentRepo(mockContext.Object);
        var dummyInformal = new DummyInformalDocDataClient();
        var spyMessageBusClient = new SpyMessageBusClient();
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AbonentProfile())));
        var controller = new AbonentsController(abonentRepo, mapper, dummyInformal, spyMessageBusClient);

        var result = controller.CreateAbonent(new AbonentCreateDto { Name = "create_Test" });

        spyMessageBusClient.NumberOfCalls.Should().Be(1);
        spyMessageBusClient.EventName.Should().Be("Abonent_Created");
    }
}