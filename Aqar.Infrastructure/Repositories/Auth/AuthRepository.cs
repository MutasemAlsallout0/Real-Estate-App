using Aqar.Core.DTOS.Auth.Request;
using Aqar.Core.DTOS.Auth.Response;
using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Exceptions;
using Aqar.Infrastructure.HelperServices.EmailHelper;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly AqarDbContext _context;
        private readonly IEmailService _emailService;
    
      //  public enum EmailCategories { Registration, ForgetPassword, VerifyEmail } //Sending email in the different situations

        public AuthRepository(IConfiguration configuration, UserManager<AppUser> userManager,
            IMapper mapper, AqarDbContext context,IEmailService emailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
            //_gettoken = gettoken;
            
        }


        public async Task<AuthenticationResponse> RegisterUserAsync(RegisterRequest model)
        {

            //var checkFromDbUser = _userManager.FindByEmailAsync(model.Email);

            //if (checkFromDbUser is not null) throw new Exception("You are already exist !");
            var emailDto = new EmailDto();

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
                 //  UserImage = model.UserImage,
                    EmailConfirmed = false,


                };
                // Generate email verification token
                var token = await Generate(user);

                // Build the email verification URL
                var verificationUrl = $"https://your-domain.com/verify-email?userId={Uri.EscapeDataString(user.Id)}&token={Uri.EscapeDataString(token.Token)}";

                emailDto.To = user.Email;
                emailDto.Subject = "Thank you for the registration";
                emailDto.Body = $"Click the button below to verify your email and activate your account:<br/><br/><a href=\"{verificationUrl}\" style=\"display:inline-block;background-color:#007bff;color:#fff;text-decoration:none;padding:10px 20px;border-radius:4px;\">Verify Email</a>";
                _emailService.SendEmail(emailDto);
                user.EmailConfirmed = true; 
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesName.OfficeOwner);

                 }

                

                return new AuthenticationResponse
                {
                    Token = token.Token, // Include the email verification token in the response
                    LoginUrl = "https://your-domain.com/login" // URL of the login page
                };

                //SendEmail(user,{ });
               // return (token);
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
                  //  UserImage= model.UserImage,
                    EmailConfirmed = false,

                };
                var token = await Generate(user);
                var verificationUrl = $"https://your-domain.com/verify-email?userId={Uri.EscapeDataString(user.Id)}&token={Uri.EscapeDataString(token.Token)}";

                emailDto.To = user.Email;
                emailDto.Subject = "Thank you for the registration";
                emailDto.Body = $"Click the button below to verify your email and activate your account:<br/><br/><a href=\"{verificationUrl}\" style=\"display:inline-block;background-color:#007bff;color:#fff;text-decoration:none;padding:10px 20px;border-radius:4px;\">Verify Email</a>";
                _emailService.SendEmail(emailDto);
                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesName.Customer);
                 }



                return new AuthenticationResponse
                {
                    Token = token.Token, // Include the email verification token in the response
                    LoginUrl = "https://your-domain.com/login" // URL of the login page
                };
            }

            throw  new ServiceValidationException("Please check information Register");
          
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
                 throw new ServiceValidationException("Login failed. Please check your Email or password.");
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



        public async Task<bool> SendResetPasswordEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // يمكنك هنا التعامل مع رسالة الخطأ المناسبة إذا كان البريد غير صحيح أو غير مؤكد
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var emailDto = new EmailDto();
            // إرسال رابط إعادة تعيين كلمة المرور عبر البريد
            var resetLink = $"https://your-domain.com/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            emailDto.To = user.Email;
            emailDto.Subject = "إعادة تعيين كلمة المرور";
            emailDto.Body = $"يرجى النقر على الرابط التالي لإعادة تعيين كلمة المرور: {resetLink}";
             _emailService.SendEmail(emailDto);

            return true;
        }


        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // يمكنك هنا التعامل مع رسالة الخطأ المناسبة إذا كان البريد غير مسجل
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                // يمكنك هنا إجراءات إضافية بمجرد إعادة تعيين كلمة المرور بنجاح
                return true;
            }

            // يمكنك هنا التعامل مع أخطاء إعادة تعيين كلمة المرور
            return false;
        }
    



    //private void SendEmail(AppUser user, EmailCategories emailCategories)
    //    {
    //        var emailDto = new EmailDto();
    //        emailDto.To = user.Email;

    //        switch (emailCategories)
    //        {
    //            case EmailCategories.Registration:
    //                {
    //                    emailDto.Subject = "Thank you for the registration";
    //                    emailDto.Body = $"This is your verify token : {Generate(user)}";
    //                    break;
    //                }
    //            case EmailCategories.ForgetPassword:
    //                {
    //                    emailDto.Subject = "You can reset your password now";
    //                    emailDto.Body = $"This is your reset token : {Generate(user)}. \n Please reset your password before {Generate(user)}";
    //                    break;
    //                }
    //            case EmailCategories.VerifyEmail:
    //                {
    //                    emailDto.Subject = "Thanks for confirming your email";
    //                    emailDto.Body = $"Your account has been verified at : {user.EmailConfirmed}";
    //                    break;
    //                }
    //            default:
    //                break;
    //        }

    //        _emailService.SendEmail(emailDto);
    //    }


        
    }
}