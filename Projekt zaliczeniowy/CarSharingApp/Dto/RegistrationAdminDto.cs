using CarSharingApp.Enums;
using CarSharingApp.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Dto
{
    public class RegistrationAdminDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [LoginValidation]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailValidation]
        public string Email { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public Roles Roles { get; set; } = Roles.Admin;
    }
}
