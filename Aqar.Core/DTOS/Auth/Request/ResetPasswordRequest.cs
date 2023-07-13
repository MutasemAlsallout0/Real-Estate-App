using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aqar.Core.DTOS.Auth.Request
{
    public class ResetPasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}