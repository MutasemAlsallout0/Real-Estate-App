using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aqar.Core.Enums
{
    public enum UserType
    {
        [Display(Name="Admin")]
        Admin=1,
        [Display(Name = "Customer")]

        Customer = 2,
        [Display(Name = "OfficeOwner")]
        OfficeOwner = 3


    }
}