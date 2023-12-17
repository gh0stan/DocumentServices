using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        [Required]
        public int Id { get; protected set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
