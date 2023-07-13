using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Core.Helpers
{
    public class Pagination
    {
        public int PerPage { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
        public int Total { get; set; }
    }
}