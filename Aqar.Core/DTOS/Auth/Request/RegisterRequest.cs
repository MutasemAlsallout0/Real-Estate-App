using Aqar.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Aqar.Core.DTOS.Auth.Request
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }  
  
        public string LastName { get; set; }
        public UserType UserType { get; set; }

        public string UserImage { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string? OfficeName { get; set; }


    }
}