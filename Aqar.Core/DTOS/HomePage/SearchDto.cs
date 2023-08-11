using Aqar.Core.Enums;

namespace Aqar.Core.DTOS.HomePage
{
    public class SearchDto
    {
        public int streetId { get; set; }
        public int cityId { get; set; }
        public int countryId { get; set; }
        public EstateType estateType { get; set; }
        public ContractType contractType { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}