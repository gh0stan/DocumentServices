using AbonentService.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace AbonentService.Dtos
{
    public class AbonentCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
