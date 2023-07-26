using Aqar.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aqar.Data.Model
{

    public class AppUser : IdentityUser 
    {
        [StringLength(50)]
        public string FirstName { get; set; } 
        [StringLength(50)]
        public string LastName { get; set; } 
        public bool IsActive { get; set; }

        public UserType UserType { get; set; }

 
        public string? UserImage { get; set; }  
        public string? OfficeName {get; set; } 
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

        public List<Following> Followings { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

    }
}
