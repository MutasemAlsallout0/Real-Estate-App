using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Core.DTOS.Auth.Request
{
    public class ConfirmPasswordResetRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
     

    }
}