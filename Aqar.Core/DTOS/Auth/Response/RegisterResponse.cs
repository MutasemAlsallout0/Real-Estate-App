using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Core.DTOS.Auth.Response
{
    public class RegisterResponse
    {
        public string Email { get; set; }

        public string token { get; set; }
    }
}