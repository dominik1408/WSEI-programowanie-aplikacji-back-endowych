using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CarSharingApp.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        public int UserId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public User? User { get; set; }
    }
}
