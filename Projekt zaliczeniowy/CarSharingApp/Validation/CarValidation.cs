using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Validation
{
    public class CarValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var entity = _context.Cars.SingleOrDefaultAsync(a => a.RegistrationNumber == value.ToString());

            if (entity == null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }
        public string GetErrorMessage(string RegistrationNumber)
        {
            return $"{RegistrationNumber} is already in use.";
        }
    }
}
