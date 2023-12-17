using AbonentService.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace AbonentService.Dtos
{
    public class AbonentCreatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Event { get; set; }
    }
}
