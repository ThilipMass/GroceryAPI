using System.ComponentModel.DataAnnotations;
namespace practice.models;
public class Login
{
    [Required]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid Name. Only alphabetic characters and spaces are allowed.")]
    public string Name { get; set; }

    [Required]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid phone number. It must start with 6-9 and contain 10 digits.")]
    public string PhoneNumber { get; set; }
}