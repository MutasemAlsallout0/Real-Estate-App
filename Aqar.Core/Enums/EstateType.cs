
using System.ComponentModel.DataAnnotations;

namespace Aqar.Core.Enums
{
    public enum EstateType
    {
        [Display(Name = "بناية")]
        Building =1,
        [Display(Name = "منزل")]
        Appartment =2,
        [Display(Name = "مخزن")]
        Warehouse =3,
        [Display(Name = "أرض")]
        Land =4,
        [Display(Name = "شاليه")]
        Chalet =5,
    }
}