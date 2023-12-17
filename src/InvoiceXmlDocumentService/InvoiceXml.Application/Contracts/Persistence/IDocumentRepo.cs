using InvoiceXml.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Contracts.Persistence
{
    public interface IDocumentRepo : IAsyncRepo<Document>
    {
        Task<IEnumerable<Document>> GetDocumentsByAbonentAsync(int abonentId);
        Task<Document> GetDocumentByGuidAsync(string docGuid);
    }
}
