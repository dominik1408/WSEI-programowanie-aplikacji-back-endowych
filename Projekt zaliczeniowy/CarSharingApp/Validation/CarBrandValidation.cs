using System.ComponentModel.DataAnnotations;

namespace CarSharingApp.Validation
{
    public class CarBrandValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var entity = _context.CarBrands.SingleOrDefault(a => a.Name == value.ToString());
            if(entity != null)
            {
                return new ValidationResult(GetErrorMeesage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        public string GetErrorMeesage(string BrandName)
        {
            return $"{BrandName} already exist in database";
        }
    }
}
