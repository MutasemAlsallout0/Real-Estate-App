using Aqar.Core.Enums;

namespace Aqar.Core.DTOS.Estate
{
    public class GetEstateDto
    {
        public int Id { get; set; }
        public bool IsOwner { get; set; }
        public DateTime CreateAt { get; set; }
        public string UserId { get; set; }
        public string OwnerEstate { get; set; }
        public string PhoneNumber { get; set; }
        public string? UserImage { get; set; }
        public string EstateType { get; set; }
        public string ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }  
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string UserType { get; set; }

        public string MainImage { get; set; }
        public List<string> Images { get; set; }
    }
}