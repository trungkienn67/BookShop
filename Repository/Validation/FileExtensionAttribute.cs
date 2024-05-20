using System.ComponentModel.DataAnnotations;


namespace BookStore.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile file) 
            {
                var extension = Path.GetExtension(file.FileName); //1.jpg
                string[] extensions = { "jpg", "png", "jpeg" };

                bool result = extensions.Any(x => extension.EndsWith(x));

                if (result)
                {
                    return ValidationResult.Success;
                }
                {
                    return new ValidationResult("Allowed extensions are jpg or png or jpeg");

                }
            }
            return ValidationResult.Success;
        }
    }
}
