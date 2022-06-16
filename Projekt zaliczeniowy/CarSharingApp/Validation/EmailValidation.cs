using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Validation
{
    public class EmailValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var entity = _context.Users.SingleOrDefaultAsync(a => a.Email == value.ToString());
            if(entity == null)
            {
                return new ValidationResult(GetErrorMesage(value.ToString()));
            }
            return ValidationResult.Success;

        }

        public string GetErrorMesage(string Email)
        {
            return $"{Email} is already in use";
        }
    }
}
