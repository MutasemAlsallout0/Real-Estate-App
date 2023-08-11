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
        public List<GetEstateDto> OfficeEstate { get; set; }
        public List<GetEstateDto> LandEstate { get; set; }
        public List<GetEstateDto> ChaletEstate { get; set; }
        public List<GetEstateDto> StoreEstate { get; set; }
        public List<GetEstateDto> RoomEstate { get; set; }
        public List<GetEstateDto> GarageEstate { get; set; }

    }
}
