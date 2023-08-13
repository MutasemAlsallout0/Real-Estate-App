using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aqar.Core.DTOS.Auth.Request
{
    public class UpdateOfficeOwnerDto
    {
        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public IFormFile? UserImage { get; set; }

        public string? PhoneNumber { get; set; }

        public string? OfficeName { get; set; }

    }
}