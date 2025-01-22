using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace practice.models;

    public class RegisterUserDTO
    {

        public string Name { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public double WalletBalance { get; set; }

        public string Role { get; set; }

    }
