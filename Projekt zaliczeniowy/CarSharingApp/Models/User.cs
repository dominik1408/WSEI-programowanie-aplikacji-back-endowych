using CarSharingApp.Enums;
using CarSharingApp.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CarSharingApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Surname { get; set; }
        [Required]
        [LoginValidation]
        [Column(TypeName = "varchar(50)")]
        public string Login { get; set; }
        [Required]
        [Column (TypeName = "varchar(255)")]
        public string Password { get; set; }
        [Required]
        [EmailValidation]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public Roles Roles { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Loan>? Loan { get; set; }
    }
}
