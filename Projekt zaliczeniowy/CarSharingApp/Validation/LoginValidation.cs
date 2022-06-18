using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Validation
{
    public class LoginValidation : ValidationAttribute 
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var entity = _context.Users.SingleOrDefault(a => a.Login == value.ToString());
            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage(string Login)
        {
            return $"{Login} is already in use.";
        }
    }
}
