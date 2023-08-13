using System.ComponentModel.DataAnnotations;

namespace Aqar.Core.DTOS.Auth.Request
{
    public class ChangePasswordDto
    {

        [DataType(DataType.Password)]
        public string oldPassword { get; set; }

        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Password and confirm password do not match.")]
        public string confirmNewPassword { get; set; }
    }
}