using InvoiceXml.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Domain.Models
{
    public class Abonent : EntityBase
    {
        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; } = new List<User>();
    }
}
