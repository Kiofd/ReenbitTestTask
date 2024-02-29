
using System.ComponentModel.DataAnnotations;

namespace Test_Task_.Net_Reenbit_Masksym_Sheremeta.Models.ValidationAttributes;

public class AllowedExtensionAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    
    public AllowedExtensionAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_extensions.Contains(extension))
            {
                return new ValidationResult($"Only {string.Join(", ", _extensions)} files are allowed.");
            }

            if (file.Length / 1024 / 1024 > 4 ) // converting length to Mb
            {
                return new ValidationResult($"The file is too large. The maximum file size is 4 Mb");
            }
        }

        return ValidationResult.Success;
    }
}