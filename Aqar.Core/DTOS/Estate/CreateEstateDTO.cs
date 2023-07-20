using Aqar.Core.Enums;

namespace Aqar.Core.DTOS.Estate
{
    public class CreateEstateDTO
    {
        public bool ActiveSate { get; set; }
        public EstateType EstateType { get; set; }
        public ContractType ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }
        public bool SeenByAdmin { get; set; }
        public string UserId { get; set; }
        public int AddressId { get; set; }
    }
}