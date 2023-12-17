using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Features.Queries.GetDocuments
{
    public class GetDocumentsQuery : IRequest<List<DocumentListItem>>
    {
        public int AbonentId { get; set; }

        public GetDocumentsQuery(int abonentId)
        {
            AbonentId = abonentId;
        }
    }
}
