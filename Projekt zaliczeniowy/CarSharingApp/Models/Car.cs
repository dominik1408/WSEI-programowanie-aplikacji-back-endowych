using CarSharingApp.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CarSharingApp.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        [CarValidation]
        [Column(TypeName = "nvarchar(7)")]
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
        [JsonIgnore]
        [IgnoreDataMember]
        public CarBrand CarBrand { get; set; }
        [ForeignKey("CarModel")]
        [Required]
        public int CarModelId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public CarModel CarModel { get; set; }
        [ForeignKey("Color")]
        [Required]
        public int ColorId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Color Color { get; set; }
    }
}
