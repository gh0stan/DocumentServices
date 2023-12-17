using AbonentService.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace AbonentService.Dtos
{
    public class AbonentReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
