using InvoiceXml.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceXml.Domain.Models
{
    public class Document : EntityBase
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string DocumentXml { get; set; }

        [Required]
        public int SenderAbonentId { get; set; }

        [Required]
        public int ReceiverAbonentId { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public string Guid { get; set; }

        [Required]
        public decimal InvoiceTotal { get; set; }
                                                                                                  
        [Required]
        public int CreatedBy { get; set; }

    }
}
