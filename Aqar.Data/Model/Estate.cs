using Aqar.Core.Enums;

namespace Aqar.Data.Model
{
    public class Estate : BaseEntity
    {
        public bool ActiveSate { get; set; }    
        public EstateType EstateType { get; set; }  
        public ContractType ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; } = string.Empty;

         public bool SeenByAdmin { get; set; }
        public List<Attachment> Images { get; set; }
        public string UserId { get; set; }  
        public AppUser? User { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }


    }
}