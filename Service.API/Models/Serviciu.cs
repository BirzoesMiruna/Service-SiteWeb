using System.ComponentModel.DataAnnotations;

namespace Service.API.Models
{
    public class Serviciu
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ? Nume { get; set; }

        public string ? Descriere { get; set; }

        public string ? ImagineUrl { get; set; } 
    }
}
