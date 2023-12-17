using InvoiceXml.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Domain.Models
{
    public class AppSettings : EntityBase
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
