using EllipticCurve.Utils;
using InvoiceXml.Application.Contracts.Persistence;
using InvoiceXml.Domain.Models;
using InvoiceXml.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Infrastructure.Repositories
{
    public class DocumentRepo : RepositoryBase<Document>, IDocumentRepo
    {
        public DocumentRepo(DataContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<Document>> GetAllDocumentsAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<Document> GetDocumentByGuidAsync(string docGuid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Document>> GetDocumentsByAbonentAsync(int abonentId)
        {
            return await _dbContext.Documents.Where(d => d.SenderAbonentId == abonentId 
                                                         || d.ReceiverAbonentId == abonentId).ToListAsync();
        }
    }
}
