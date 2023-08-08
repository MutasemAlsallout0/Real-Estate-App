
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Aqar.Core.Enums
{
    public enum ContractType
    {
        [Display(Name = "ملك")]
        Selling = 1,
        [Display(Name = "اجار")]
        Rent = 2,

    }
}