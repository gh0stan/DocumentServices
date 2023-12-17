using MediatR;

namespace InvoiceXml.Application.Features.Commands.CreateDocument
{
    public class CreateDocumentCommand : IRequest<int>
    {
        public string DocumentXml { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }
        
        public int UserId { get; set; }

        public int SenderAbonentId { get; set; }

        public int ReceiverAbonentId { get; set; }

        public string Guid { get; set; }

        public decimal InvoiceTotal { get; set; }
    }
}
