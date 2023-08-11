
using System.ComponentModel.DataAnnotations;

namespace Aqar.Core.Enums
{
    public enum EstateType
    {
        [Display(Name = "منزل")]
        Building =1,
        [Display(Name = "شقة")]
        Appartment =2,
        [Display(Name = "كراج")]
        Garage = 3,
        [Display(Name = "أرض")]
        Land =4,
        [Display(Name = "شاليه")]
        Chalet =5,
        [Display(Name = "مكتب")]
        Office = 6,
        [Display(Name = "متجر")]
        Store = 7,
        [Display(Name = "غرفة")]
        Room = 8,

 
    }
}