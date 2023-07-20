
using System.ComponentModel.DataAnnotations;

namespace Aqar.Core.Enums
{
    public enum EstateType
    {
        [Display(Name = "بناية")]
        Building =0,
        [Display(Name = "منزل")]
        Appartment =1,
        [Display(Name = "مخزن")]
        Warehouse =2,
        [Display(Name = "أرض")]
        Land =3,
        [Display(Name = "شاليه")]
        Chalet =4,
    }
}