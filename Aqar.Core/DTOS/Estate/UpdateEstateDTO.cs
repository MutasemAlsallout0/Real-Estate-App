using Aqar.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Aqar.Core.DTOS.Estate
{
    public class UpdateEstateDTO
    {
        public int Id { get; set; }
        public EstateType EstateType { get; set; }
        public ContractType ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }
        public bool SeenByAdmin { get; set; }
        public List<IFormFile> Imagesfile { get; set; }
        public int StreetId { get; set; }
    }
}