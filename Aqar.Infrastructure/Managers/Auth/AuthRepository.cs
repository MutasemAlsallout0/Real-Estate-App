using Aqar.Core.DTOS.Auth.Request;
using Aqar.Core.DTOS.Auth.Response;
using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Exceptions;
using Aqar.Infrastructure.HelperServices.EmailHelper;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aqar.Infrastructure.Managers.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        //private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly AqarDbContext _context;
        private readonly IEmailService _emailService;
        public enum EmailCategories { Registration, ForgetPassword, VerifyEmail } //Sending email in the different situations

        public AuthRepository(IConfiguration configuration, UserManager<AppUser> userManager,
            IMapper mapper, AqarDbContext context,IEmailService emailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
        }

        public async Task<AuthenticationResponse> RegisterUserAsync(RegisterRequest model)
        {
 
             if (model.UserType == UserType.OfficeOwner)
            {
                var user = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    UserType = UserType.OfficeOwner,
                   PhoneNumber = model.PhoneNumber,
                   OfficeName = model.OfficeName,
                   UserImage = model.UserImage,

                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesName.OfficeOwner);

                 }

                var token = await Generate(user);

                  return (token);
            }
            else if (model.UserType == UserType.Customer)
            {
                var user = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    UserType = UserType.Customer,
                    PhoneNumber = model.PhoneNumber,
                    UserImage= model.UserImage,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesName.Customer);
                 }

                var token = await Generate(user);

                return (token);
            }

            throw  new ServiceValidationException("Invlid inserted data");

        }
        
        public async Task<AuthenticationResponse> Login(LoginRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.email);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.password))
            {

                var token = await Generate(user);
                return (token);

            }
            else
                 throw new ServiceValidationException(404, "Unauthorized");
        }

        public async Task<AuthenticationResponse> Generate(AppUser user)
        {
            // Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,"Id"),
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim("Email" , user.Email),


            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //SecretKey
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secert"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var Expiration = DateTime.Now.AddMinutes(1);


            // token 
            var token = new JwtSecurityToken(
               issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
               claims: claims,
               expires: Expiration,
               signingCredentials: cred);
             
            return new AuthenticationResponse
            {
                Token = $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}",
                Expiration = Expiration
            };
        }


        //private void SendEmail(AppUser user, EmailCategories emailCategories)
        //{
        //    var emailDto = new EmailDto();
        //    emailDto.To = user.Email;

        //    switch (emailCategories)
        //    {
        //        case EmailCategories.Registration:
        //            {
        //                emailDto.Subject = "Thank you for the registration";
        //                emailDto.Body = $"This is your verify token : {user.VerificationToken}";
        //                break;
        //            }
        //        case EmailCategories.ForgetPassword:
        //            {
        //                emailDto.Subject = "You can reset your password now";
        //                emailDto.Body = $"This is your reset token : {user.PasswordResetToken}. \n Please reset your password before {user.ResetTokenExpires}";
        //                break;
        //            }
        //        case EmailCategories.VerifyEmail:
        //            {
        //                emailDto.Subject = "Thanks for confirming your email";
        //                emailDto.Body = $"Your account has been verified at : {user.VerifiedAt}";
        //                break;
        //            }
        //        default:
        //            break;
        //    }

        //    _emailService.SendEmail(emailDto);
        //}

        private string GenratePassword()
        {
            return Guid.NewGuid().ToString().Substring(1, 8);
        }

        public Task<AuthenticationResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}