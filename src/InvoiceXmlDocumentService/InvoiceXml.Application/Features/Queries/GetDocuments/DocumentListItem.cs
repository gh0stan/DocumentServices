using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Features.Queries.GetDocuments
{
    public class DocumentListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime DocumentDate { get; set; }
    }
}
