using System.ComponentModel.DataAnnotations;

namespace InformalDocumentService.Data.Models
{
    public class AppSettings
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
