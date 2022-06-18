using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Validation
{
    public class CarModelValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var entity = _context.CarModels.SingleOrDefault(a => a.Name == value.ToString());
            if(entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage(string CarBrand)
        {
            return $"{CarBrand} already exist in database";
        }
    }
}
