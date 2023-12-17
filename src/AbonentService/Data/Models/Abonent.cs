using System.ComponentModel.DataAnnotations;

namespace AbonentService.Data.Models
{
    public class Abonent
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public ICollection<Address> Addresses { get; } = new List<Address>();
    }
}
