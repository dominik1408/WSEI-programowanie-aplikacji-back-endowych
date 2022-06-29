using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CarSharingApp.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        [Required]
        public DateTime LoanDateStart { get; set; }
        [Required]
        public DateTime LoanDateEnd { get; set; }
        [Required]
        [ForeignKey("Car")]
        public int CarId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Car? Car { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public User? User { get; set; }  
        [Required]
        [ForeignKey("LoanType")]
        public int LoanTypeId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public LoanType? LoanType { get; set; }  

    }
}
