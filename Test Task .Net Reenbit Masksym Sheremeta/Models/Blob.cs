using System.ComponentModel.DataAnnotations;
using Test_Task_.Net_Reenbit_Masksym_Sheremeta.Models.ValidationAttributes;

namespace Test_Task_.Net_Reenbit_Masksym_Sheremeta.Models;

public class Blob
{
    [Required]
    [EmailValidation]
    public string? Email { get; set; }
    
    [Required]
    [AllowedExtension([".docx"])]
    public IFormFile? File { get; set; }
}   