using Aqar.Core.DTOS.Estate;

namespace Aqar.Core.DTOS.HomePage
{
    public class HomePageData
    {
        public EstateData GetEstateData { get; set; }




    }

   public class EstateData
    {
        public List<EstateDTO> BuildingEstate { get; set; }
        public List<EstateDTO> AppartmentEstate { get; set; }
        public List<EstateDTO> WarehouseEstate { get; set; }
        public List<EstateDTO> LandEstate { get; set; }
        public List<EstateDTO> ChaletEstate { get; set; }
    }
}
