using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Models
{
    public class CarBrand
    {
        [Key]
        public int CarBrandId { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
