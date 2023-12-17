using System.ComponentModel.DataAnnotations;

namespace InformalDocumentService.Data.Models
{
    public class Document
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required] 
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string Guid { get; set; }
    }
}
