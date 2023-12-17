using InformalDocumentService.Data.Models;

namespace InformalDocumentService.Data.Repositories
{
    public interface IDocumentRepo
    {
        bool SaveChanges();

        void CreateAbonent(Abonent abonent);
        IEnumerable<Abonent> GetAllAbonents();
        bool AbonentExists(int extAbonentId);

        void CreateDocument(int senderId, Document document);
        IEnumerable<Document> GetAllSentDocuments(int abonentId);
        IEnumerable<Document> GetAllReceivedDocuments(int abonentId);
        Document GetDocumentById(int Id);
        Document GetDocumentByGuid(string docGuid);


    }
}
