using Aqar.Core.Enums;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Aqar.Core.DTOS.Estate
{
    public class GetEstateDto
    {
        public int Id { get; set; }
        public string OwnerEstate { get; set; }

        public string EstateType { get; set; }
        public string ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }  

        public bool SeenByAdmin { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public List<string> Images { get; set; }
    }
}