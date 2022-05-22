using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSharingApp.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public double MeterStatus { get; set; }
        [Required]
        public int ProductionYear { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [ForeignKey("CarBrand")]
        [Required]
        public int CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }
        [ForeignKey("CarModel")]
        [Required]
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; }
        [ForeignKey("Color")]
        [Required]
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
