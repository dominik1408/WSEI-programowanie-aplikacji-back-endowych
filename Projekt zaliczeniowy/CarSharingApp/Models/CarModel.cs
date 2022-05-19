using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Models
{
    public class CarModel
    {
        [Key]
        public int CarModelId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
