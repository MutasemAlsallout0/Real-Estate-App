using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Aqar.Core.DTOS.Auth.Request
{
    public class UpdateCustomerrDto
    {
        public string UserId { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IFormFile UserImage { get; set; }

        public string PhoneNumber { get; set; }


    }
}