using System.ComponentModel.DataAnnotations;

namespace InformalDocumentService.Dtos
{
    public class AbonentReadDto
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
    }
}
