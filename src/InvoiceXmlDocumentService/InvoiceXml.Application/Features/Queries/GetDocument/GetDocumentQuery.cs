using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Features.Queries.GetDocument
{
    public class GetDocumentQuery : IRequest<DocumentItem>
    {
        public string DocumentGuid { get; set; }

        public GetDocumentQuery(string documentGuid)
        {
            DocumentGuid = documentGuid;
        }
    }
}
