using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Core.DTOS.Auth.Request
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}