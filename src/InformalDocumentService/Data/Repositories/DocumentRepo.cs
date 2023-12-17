using InformalDocumentService.Data.Models;

namespace InformalDocumentService.Data.Repositories
{
    public class DocumentRepo : IDocumentRepo
    {
        private readonly DataContext _context;

        public DocumentRepo(DataContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool AbonentExists(int extAbonentId)
        {
            return _context.Abonents.Any(a => a.ExternalId == extAbonentId);
        }

        public void CreateAbonent(Abonent abonent)
        {
            if (abonent == null)
            {
                throw new ArgumentNullException(nameof(abonent));
            }

            _context.Abonents.Add(abonent);
        }

        public void CreateDocument(int senderId, Document document)
        {
            if (document == null) 
            { 
                throw new ArgumentNullException(nameof(document));
            }

            document.CreatedOn = DateTime.Now;
            document.Guid = Guid.NewGuid().ToString();
            document.SenderId = senderId;

            _context.Documents.Add(document);
        }

        public IEnumerable<Abonent> GetAllAbonents()
        {
            return _context.Abonents.ToList();
        }

        public IEnumerable<Document> GetAllReceivedDocuments(int abonentId)
        {
            return _context.Documents.Where(d => d.ReceiverId == abonentId).ToList();
        }

        public IEnumerable<Document> GetAllSentDocuments(int abonentId)
        {
            return _context.Documents.Where(d => d.SenderId == abonentId).ToList();
        }


        public Document GetDocumentByGuid(string docGuid)
        {
            return _context.Documents.FirstOrDefault(d => d.Guid == docGuid);
        }

        public Document GetDocumentById(int id)
        {
            return _context.Documents.FirstOrDefault(d => d.Id == id);
        }
    }
}
