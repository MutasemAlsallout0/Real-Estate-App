using Aqar.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Aqar.Core.DTOS.Auth.Request
{
    public class RegisterRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }  
  
        public string LastName { get; set; }
        public UserType UserType { get; set; }

       // public string UserImage { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string? OfficeName { get; set; }


    }
}