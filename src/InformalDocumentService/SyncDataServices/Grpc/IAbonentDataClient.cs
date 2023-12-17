using InformalDocumentService.Data.Models;

namespace InformalDocumentService.SyncDataServices.Grpc
{
    public interface IAbonentDataClient
    {
        IEnumerable<Abonent> GetAllAbonents();
    }
}
