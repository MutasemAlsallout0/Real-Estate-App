using Aqar.Core.DTOS.Auth.Request;
using Aqar.Core.DTOS.Auth.Response;
using Aqar.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace Aqar.Infrastructure.Managers.Auth
{
    public interface IAuthRepository
    {
        Task<AuthenticationResponse> Generate(AppUser user);
         Task<AuthenticationResponse> RegisterUserAsync(RegisterRequest model);
        Task<AuthenticationResponse> Login(LoginRequest request);
        Task<bool> SendResetPasswordEmailAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}