using Aqar.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string MainImage { get; set; }

        public List<Attachment> Images { get; set; }
        public string UserId { get; set; }
        public AppUser? User { get; set; }
        public int StreetId { get; set; }
        public Street Street { get; set; }



        [NotMapped]
        public string DisplayEstateType
        {
            get
            {
                switch (EstateType)
                {
                    case EstateType.Building:
                        return "بناية";
                    case EstateType.Appartment:
                        return "منزل";
                    case EstateType.Warehouse:
                        return "مخزن";
                    case EstateType.Land:
                        return "أرض";
                    case EstateType.Chalet:
                        return "شاليه";
                    default:
                        return EstateType.ToString();
                }
            }
        }


        [NotMapped]
        public string DisplayContractType
        {
            get
            {
                switch (ContractType)
                {
                    case ContractType.Selling:
                        return "ملك";
                    case ContractType.Rent:
                        return "اجار";
                    default:
                        return EstateType.ToString();
                }
            }
        }

    }
}