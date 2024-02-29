using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Test_Task_.Net_Reenbit_Masksym_Sheremeta.Models.ValidationAttributes;

public class EmailValidationAttribute : ValidationAttribute
{
    private const string _regex =
        "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string email)
        {
            Regex regex = new Regex(_regex);
            
            Match match = regex.Match(email);
            if (match.Success)
                return ValidationResult.Success;
            else
                return new ValidationResult($"Wrong email");
        }
        return new ValidationResult($"Invalid input");
    }
}