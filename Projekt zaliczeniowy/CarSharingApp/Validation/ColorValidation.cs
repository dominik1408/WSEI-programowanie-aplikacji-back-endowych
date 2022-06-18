using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Validation
{
    public class ColorValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var enitiy = _context.Colors.SingleOrDefault(a => a.ColorName == value.ToString());

            if (enitiy != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }
        public string GetErrorMessage(string Color)
        {
            return $"{Color} already exist in database.";
        }

    }
}
