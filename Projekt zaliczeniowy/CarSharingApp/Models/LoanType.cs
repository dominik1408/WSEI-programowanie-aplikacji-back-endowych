using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CarSharingApp.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }
        [Required]
        [Column(TypeName ="decimal(6,2)")]
        public double Price { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Loan>? Loan { get; set; }
    }
}
