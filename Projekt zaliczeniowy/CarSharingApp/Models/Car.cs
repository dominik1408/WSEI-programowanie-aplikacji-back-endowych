using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSharingApp.Models
{
    public class Car
    {
        [Key]
        public int CarId{ get; set; }
        public string RegistrationNumber { get; set; }
        public double MeterStatus { get; set; }
        public int ProductionYear { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("CarBrand")]
        public int CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }
        [ForeignKey("CarModel")]
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; }
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
