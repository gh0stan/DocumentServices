using AbonentService.Dtos;
using AbonentService.SyncDataServices.Http;

namespace AbonentService.Api.Tests.TestDoubles;

public class DummyInformalDocDataClient : IInformalDocDataClient
{
    public Task SendAbonentToInformalDocService(AbonentReadDto abonentReadDto)
    {
        return null;
    }
}