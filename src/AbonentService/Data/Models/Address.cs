using System.ComponentModel.DataAnnotations;

namespace AbonentService.Data.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int AbonentId { get; set; }

        public Abonent Abonent { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }

        public string? City { get; set; }
        
        public string? Street { get; set; }
        
        public string? Building { get; set; }
    }
}
