using System.ComponentModel.DataAnnotations;

namespace InformalDocumentService.Dtos
{
    public class DocumentCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }
    }
}
