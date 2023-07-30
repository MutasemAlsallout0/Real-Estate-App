using Aqar.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Aqar.Core.DTOS.Estate
{
    public class CreateEstateDTO
    {
        public EstateType EstateType { get; set; }
        public ContractType ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Imagesfile { get; set; }
        public string UserId { get; set; }
        public int StreetId { get; set; }
    }
}