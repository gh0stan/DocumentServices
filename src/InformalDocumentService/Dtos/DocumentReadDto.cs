using System.ComponentModel.DataAnnotations;

namespace InformalDocumentService.Dtos
{
    public class DocumentReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid Guid { get; set; }
    }
}
