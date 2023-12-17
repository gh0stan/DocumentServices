using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Features.Commands.DeleteDocument
{
    public class DeleteDocumentCommand : IRequest
    {
        public int Id { get; set; }
    }
}
