using Aqar.Core.Enums;

namespace Aqar.Data.Model
{
    public class Estate : BaseEntity
    {
        public EstateType EstateType { get; set; }  
        public ContractType ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; } = string.Empty;

         public bool SeenByAdmin { get; set; }
        public List<Attachment> Images { get; set; }
        public string UserId { get; set; }  
        public AppUser? User { get; set; }

        public int StreetId { get; set; }
        public Street Street { get; set; }

        //public int CountryId { get; set; }
        //public Country Country { get; set; }
        //public int CityId { get; set; }
        //public City City { get; set; }  

        //public int StreetId { get; set; }
        //public Street Street { get; set; }






    }
}