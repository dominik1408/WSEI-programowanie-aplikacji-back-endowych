using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeId { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
    }
}
