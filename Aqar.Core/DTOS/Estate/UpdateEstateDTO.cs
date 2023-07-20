using Aqar.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;

namespace Aqar.Core.DTOS.Estate
{
    public class UpdateEstateDTO
    {
        public int Id { get; set; }
        public bool ActiveSate { get; set; }
        public EstateType EstateType { get; set; }
        public ContractType ContractType { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }
        public string Description { get; set; }
        public bool SeenByAdmin { get; set; }
        public List<IFormFile> Images { get; set; }
        public string UserId { get; set; }
        public int AddressId { get; set; }
    }
}