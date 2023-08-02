using Aqar.Core.Enums;

namespace Aqar.Core.DTOS.HomePage
{
    public class SearchDto
    {
         public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public EstateType estateType { get; set; }
        public ContractType contractType { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}