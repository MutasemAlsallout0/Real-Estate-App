using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Core.Helpers
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordsPerPage { get; set; } = 10;
        private readonly int maxAmount = 20;

        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set
            {
                recordsPerPage = (value > maxAmount) ? maxAmount : value;
            }
        }
    }
}