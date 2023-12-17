using System.ComponentModel.DataAnnotations;

namespace InformalDocumentService.Data.Models
{
    public class Abonent
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
