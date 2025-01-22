using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace practice.models;

    public class User
    {
        // [Required]
        // [Key]
        // [StringLength(16, MinimumLength = 8, ErrorMessage = "Username must be between 8 and 16 characters.")]
        // [RegularExpression(@"^[A-Za-z][A-Za-z0-9]*$", ErrorMessage = "Username must start with a letter and contain only letters or numbers.")]
        // public string UserName { get; set; }
        [Key]
        public int CustomerID {get;set;}


        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid Name. Only alphabetic characters and spaces are allowed.")]
        public string Name { get; set; }


        // [Required]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""':{}|<>]).{8,16}$", ErrorMessage = "Password didn't match the criteria.")]
        // public string Password { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email Id.")]
        public string Email { get; set; }

        // [Required]
        // [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
        // public int Age { get; set; }

        [Required]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid phone number. It must start with 6-9 and contain 10 digits.")]
        public string PhoneNumber { get; set; }

        // [Required]
        // public string Address { get; set; }

        // [Required]
        // public string City { get; set; }

        // [Required]
        // public string PostalCode { get; set; }

        // [Required]
        // public string State { get; set; }

        [Required]
         public double WalletBalance { get; set; }

        [Required]
        [RegularExpression(@"^(user|admin)$", ErrorMessage = "Role must be either 'user' or 'admin'.")]
        public string Role { get; set; }

    }
