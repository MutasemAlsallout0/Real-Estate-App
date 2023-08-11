using Aqar.Core.DTOS.Estate;

namespace Aqar.Core.DTOS.HomePage
{
    public class HomePageData
    {
        public EstateData GetEstateData { get; set; }

    }

   public class EstateData
    {
        public List<GetEstateDto> BuildingEstate { get; set; }
        public List<GetEstateDto> AppartmentEstate { get; set; }
        public List<GetEstateDto> WarehouseEstate { get; set; }
        public List<GetEstateDto> LandEstate { get; set; }
        public List<GetEstateDto> ChaletEstate { get; set; }
    }
}
