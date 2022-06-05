using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public DateTime LoanDateStart { get; set; }
        public DateTime LoanDateEnd { get; set; }   
        public int CarId { get; set; }
        public int UserId { get; set; }
        public int AdminId { get; set; }
        public int LoanTypeId { get; set; }
    }
}
