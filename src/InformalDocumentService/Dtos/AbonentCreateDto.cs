using System.ComponentModel.DataAnnotations;

namespace InformalDocumentService.Dtos
{
    public class AbonentCreateDto
    {
        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
