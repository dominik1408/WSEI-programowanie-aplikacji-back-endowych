using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CarSharingApp.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        [Required]
        public string ColorName { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Car> Car { get; set; }
    }
}
