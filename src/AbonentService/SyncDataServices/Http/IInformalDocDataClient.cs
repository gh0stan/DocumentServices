using AbonentService.Dtos;

namespace AbonentService.SyncDataServices.Http
{
    public interface IInformalDocDataClient
    {
        Task SendAbonentToInformalDocService(AbonentReadDto abonentReadDto);
    }
}
