using AbonentService.Dtos;

namespace AbonentService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void CreateNewAbonent(AbonentCreatedDto abonentCreatedDto);
    }
}
