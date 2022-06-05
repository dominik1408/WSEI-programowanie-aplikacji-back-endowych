using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public int UserId { get; set; }
    }
}
