using Aqar.Core.Enums;

namespace Aqar.Core.DTOS.Estate
{
    public class EstateDTO
    {
        public int Id { get; set; }
        public bool ActiveSate { get; set; }
        public string EstateType { get; set; }
        public string ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }
        public bool SeenByAdmin { get; set; }
        public string UserId { get; set; }
        public int AddressId { get; set; }
    }
}