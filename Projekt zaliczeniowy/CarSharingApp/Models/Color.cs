using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        [Required]
        public string ColorName { get; set; }
    }
}
