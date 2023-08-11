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
        public IFormFile MainImage { get; set; }

        public List<IFormFile> Imagesfile { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string StreetName { get; set; }


    }
}