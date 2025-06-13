using System.ComponentModel.DataAnnotations;

namespace RecipeDbApi.Validation
{
    public class ValidImageAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            string url = $"{value}";
            if (string.IsNullOrEmpty(url) || url.EndsWith(".jpg") || url.EndsWith(".png") || url.EndsWith(".jpeg"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Muss vom Typ .jpg, .png oder .jpeg sein");
        }
    }
}
