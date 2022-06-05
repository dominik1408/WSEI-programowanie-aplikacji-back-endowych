using CarSharingApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; } 
        public Roles Roles { get; set; }

    }
}
