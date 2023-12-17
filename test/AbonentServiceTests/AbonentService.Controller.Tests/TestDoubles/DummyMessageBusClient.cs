using AbonentService.AsyncDataServices;
using AbonentService.Dtos;

namespace AbonentService.Api.Tests.TestDoubles;

public class DummyMessageBusClient : IMessageBusClient
{
    public void CreateNewAbonent(AbonentCreatedDto abonentCreatedDto)
    {
        return;
    }
}