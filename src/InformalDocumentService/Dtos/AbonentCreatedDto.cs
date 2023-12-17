using System.ComponentModel.DataAnnotations;

namespace InformalDocumentService.Dtos
{
    public class AbonentCreatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Event { get; set; }
    }
}
