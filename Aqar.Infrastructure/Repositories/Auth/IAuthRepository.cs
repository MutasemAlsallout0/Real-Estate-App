﻿using Aqar.Core.DTOS.ApiBase;
using Aqar.Core.DTOS.Auth.Request;
using Aqar.Core.DTOS.Auth.Response;
using Aqar.Data.Model;
using Microsoft.AspNetCore.Identity;
using static Aqar.Infrastructure.Managers.Auth.AuthRepository;

namespace Aqar.Infrastructure.Managers.Auth
{
    public interface IAuthRepository
    {
        Task<AuthenticationResponse> Generate(AppUser user);
         Task<AuthenticationResponse> RegisterCustomer(CustomerRegisterRequest model);
        Task<AuthenticationResponse> RegisterOfficeOwner(OfficeOwnerRegisterRequest model);
        Task<AuthenticationResponse> Login(LoginRequest request);
        Task<bool> SendResetPasswordEmailAsync(string email);
        Task<bool> ResetPasswordAsync(RestPAssword restPAssword);
        Task<string> ChangePassword(UserModel cuurentUser, ChangePasswordDto changePasswordDto);
        Task<string> UpdateOfficeOwner(UserModel cuurentUser, UpdateOfficeOwnerDto updateOfficeOwnerDto);
        Task<string> UpdateCustomer(UserModel cuurentUser, UpdateCustomerrDto updateCustomerrDto);
        Task<GetUserProfileDto> GetUserProfil(UserModel cuurentUser);
        Task<bool> DeleteUser(string userId);
    }
}