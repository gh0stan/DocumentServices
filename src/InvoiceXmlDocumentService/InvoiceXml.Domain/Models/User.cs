using InvoiceXml.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Domain.Models
{
    public class User : EntityBase
    {
        [Required]
        public int ExternalId { get; set; }

        public Abonent Abonent { get; set; }

        [Required]
        public int AbonentId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
