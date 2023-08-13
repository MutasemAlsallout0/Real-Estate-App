using Aqar.Core.DTOS.ApiBase;
using Aqar.Core.DTOS.Auth.Request;
using Aqar.Core.DTOS.Auth.Response;
using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Exceptions;
using Aqar.Infrastructure.HelperServices.EmailHelper;
using Aqar.Infrastructure.HelperServices.ImageHelper;
using Aqar.Infrastructure.Repositories.PublicPage;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
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
        private readonly IImageService _imageService;
        private readonly IPublicPageRepository _publicPageRepository;
        private readonly IEmailService _emailService;
    
      //  public enum EmailCategories { Registration, ForgetPassword, VerifyEmail } //Sending email in the different situations

        public AuthRepository(IConfiguration configuration, 
                            UserManager<AppUser> userManager,
                            IMapper mapper, 
                            AqarDbContext context,
                            IEmailService emailService,
                            IImageService imageService, IPublicPageRepository publicPageRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
            _imageService = imageService;
           _publicPageRepository = publicPageRepository;
        }


        public async Task<AuthenticationResponse> RegisterCustomer(CustomerRegisterRequest model)
        {

            var checkFromDbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(model.Email));

            if (checkFromDbUser != null)
            {
                if (!checkFromDbUser.IsActive)
                {
                    throw new ServiceValidationException("تم إيقاف الحساب. يرجى التواصل مع الدعم لاستعادته.");
                }

                throw new ServiceValidationException("انت لديك حساب مسبقا");
            }


            var user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                UserType = UserType.Customer,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = false,

            };
            if (model.UserImage != null)
            {
                user.UserImage = await _imageService.SaveImage(model.UserImage, "Images");
            }
            var token = await Generate(user);
            var verificationUrl = $"https://your-domain.com/verify-email?userId={Uri.EscapeDataString(user.Id)}&token={Uri.EscapeDataString(token.Token)}";
            
            var emailDto = new EmailDto();
            emailDto.To = user.Email;
            emailDto.Subject = "Thank you for the registration";
            //  emailDto.Body = $"Click the button below to verify your email and activate your account:<br/><br/><a href=\"{verificationUrl}\" style=\"display:inline-block;background-color:#007bff;color:#fff;text-decoration:none;padding:10px 20px;border-radius:4px;\">Verify Email</a>";
            emailDto.Body = $@"
        <!doctype html>
        <html lang=""en-US"">
        <head>
            <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
            <title>Confirm Account Registration</title>
            <style type=""text/css"">
                a:hover {{text-decoration: underline !important;}}
            </style>
        </head>
        <body marginheight=""0"" topmargin=""0"" marginwidth=""0"" style=""margin: 0px; background-color: #f2f3f8;"" leftmargin=""0"">
            <!--100% body table-->
            <table cellspacing=""0"" border=""0"" cellpadding=""0"" width=""100%"" bgcolor=""#f2f3f8""
                style=""@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;"">
                <tr>
                    <td>
                        <table style=""background-color: #f2f3f8; max-width:670px;  margin:0 auto;"" width=""100%"" border=""0""
                            align=""center"" cellpadding=""0"" cellspacing=""0"">
                            <tr>
                                <td style=""height:80px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style=""text-align:center;"">
                                    <a href=""https://example.com"" title=""logo"" target=""_blank"">
                                        <img width=""60"" src=""~/Repeat Grid 1.svg"" title=""logo"" alt=""logo"">
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td style=""height:20px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width=""95%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0""
                                        style=""max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);"">
                                        <tr>
                                            <td style=""height:40px;"">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style=""padding:0 35px;"">
                                                <h1 style=""color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;"">Welcome to our community!</h1>
                                                <span
                                                    style=""display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;""></span>
                                                <p style=""color:#455056; font-size:15px;line-height:24px; margin:0;"">
                                                    Thank you for registering an account with us. To activate your account, please click the following link:
                                                </p>
                                                <a href=""{verificationUrl}""
                                                    style=""background:#20e277;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;"">Confirm Account</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""height:40px;"">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            <tr>
                                <td style=""height:20px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style=""text-align:center;"">
                                    <p style=""font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;"">&copy; <strong>www.example.com</strong></p>
                                </td>
                            </tr>
                            <tr>
                                <td style=""height:80px;"">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <!--/100% body table-->
        </body>
        </html>
    ";
            _emailService.SendEmail(emailDto);
            user.EmailConfirmed = true;
            user.IsActive = true;


            // Send SMS verification
            //var verificationCode = GenerateRandomVerificationCode(); // Implement this method to generate a random verification code.
            //var twilioPhoneNumber = "+972598432320"; // Replace with your Twilio phone number
            //var twilioAccountSid = "ACd5a9ec23fdf8554132a15965cfdcbba4"; // Replace with your Twilio Account SID
            //var twilioAuthToken = "ed9bb22e0d0542fdd350280a365f11c6"; // Replace with your Twilio Auth Token

            //TwilioClient.Init(twilioAccountSid, twilioAuthToken);

            //var message = MessageResource.Create(
            //    body: $"Your verification code is: {verificationCode}",
            //    from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
            //    to: new Twilio.Types.PhoneNumber(model.PhoneNumber)
            //);

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
            throw new ServiceValidationException("تاكد من بيانات التسجيل");

        }
 
        public async Task<AuthenticationResponse> RegisterOfficeOwner(OfficeOwnerRegisterRequest model)
        {


            var checkFromDbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(model.Email.ToLower()));

            if (checkFromDbUser != null)
            {
                if (!checkFromDbUser.IsActive)
                {
                    throw new ServiceValidationException("تم إيقاف الحساب. يرجى التواصل مع الدعم لاستعادته.");
                }

                throw new ServiceValidationException("انت لديك حساب مسبقا");
            }

            var user = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    UserType = UserType.OfficeOwner,
                   PhoneNumber = model.PhoneNumber,
                   OfficeName = model.OfficeName,
                    EmailConfirmed = false,


                };
                if (model.UserImage != null)
                {
                    user.UserImage = await _imageService.SaveImage(model.UserImage, "Images");
                }
            // Generate email verification token
            var token = await Generate(user);

                // Build the email verification URL
                var verificationUrl = $"https://your-domain.com/verify-email?userId={Uri.EscapeDataString(user.Id)}&token={Uri.EscapeDataString(token.Token)}";
                var emailDto = new EmailDto();
                emailDto.To = user.Email;
                emailDto.Subject = "Thank you for the registration";
               // emailDto.Body = $"Click the button below to verify your email and activate your account:<br/><br/><a href=\"{verificationUrl}\" style=\"display:inline-block;background-color:#007bff;color:#fff;text-decoration:none;padding:10px 20px;border-radius:4px;\">Verify Email</a>";
               emailDto.Body= $@"
        <!doctype html>
        <html lang=""en-US"">
        <head>
            <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
            <title>Confirm Account Registration</title>
            <style type=""text/css"">
                a:hover {{text-decoration: underline !important;}}
            </style>
        </head>
        <body marginheight=""0"" topmargin=""0"" marginwidth=""0"" style=""margin: 0px; background-color: #f2f3f8;"" leftmargin=""0"">
            <!--100% body table-->
            <table cellspacing=""0"" border=""0"" cellpadding=""0"" width=""100%"" bgcolor=""#f2f3f8""
                style=""@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;"">
                <tr>
                    <td>
                        <table style=""background-color: #f2f3f8; max-width:670px;  margin:0 auto;"" width=""100%"" border=""0""
                            align=""center"" cellpadding=""0"" cellspacing=""0"">
                            <tr>
                                <td style=""height:80px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style=""text-align:center;"">
                                    <a href=""https://example.com"" title=""logo"" target=""_blank"">
                                        <img width=""60"" src=""https://i.ibb.co/hL4XZp2/android-chrome-192x192.png"" title=""logo"" alt=""logo"">
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td style=""height:20px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width=""95%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0""
                                        style=""max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);"">
                                        <tr>
                                            <td style=""height:40px;"">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style=""padding:0 35px;"">
                                                <h1 style=""color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;"">Welcome to our community!</h1>
                                                <span
                                                    style=""display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;""></span>
                                                <p style=""color:#455056; font-size:15px;line-height:24px; margin:0;"">
                                                    Thank you for registering an account with us. To activate your account, please click the following link:
                                                </p>
                                                <a href=""{verificationUrl}""
                                                    style=""background:#20e277;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;"">Confirm Account</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""height:40px;"">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            <tr>
                                <td style=""height:20px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style=""text-align:center;"">
                                    <p style=""font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;"">&copy; <strong>www.example.com</strong></p>
                                </td>
                            </tr>
                            <tr>
                                <td style=""height:80px;"">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <!--/100% body table-->
        </body>
        </html>
    ";
            _emailService.SendEmail(emailDto);
                user.EmailConfirmed = true; 
                user.IsActive = true;
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesName.OfficeOwner);

                 }
            var publicPage = await _publicPageRepository.CreatePublicPageForOfficeOwner(user.Id);


            return new AuthenticationResponse
                {
                    Token = token.Token, // Include the email verification token in the response
                    LoginUrl = "https://your-domain.com/login" // URL of the login page
                };       

              throw  new ServiceValidationException("تاكد من بيانات التسجيل");

        }

      
        public async Task<AuthenticationResponse> Login(LoginRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.email);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.password) && user.IsActive == true)
            {

                var token = await Generate(user);
                return (token);

            }
            else
                 throw new ServiceValidationException("يرجى التاكد من البريد الالكتروني او كلمة السر المدخلة");
        }
        

        public async Task<AuthenticationResponse> Generate(AppUser user)
        {
            // Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, $"{user.FirstName}  {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
  

            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //SecretKey
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secert"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var Expiration = DateTime.Now.AddDays(1);


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
            var resetLink = $"http://localhost:4200/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            emailDto.To = user.Email;
            emailDto.Subject = "إعادة تعيين كلمة المرور";
         //   emailDto.Body = $"يرجى النقر على الرابط التالي لإعادة تعيين كلمة المرور: {resetLink}";
         emailDto.Body = $@"
        <!doctype html>
        <html lang=""en-US"">
        <head>
            <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
            <title>Reset Password Email Template</title>
            <meta name=""description"" content=""Reset Password Email Template."">
            <style type=""text/css"">
                a:hover {{text-decoration: underline !important;}}
            </style>
        </head>
        <body marginheight=""0"" topmargin=""0"" marginwidth=""0"" style=""margin: 0px; background-color: #f2f3f8;"" leftmargin=""0"">
            <!--100% body table-->
            <table cellspacing=""0"" border=""0"" cellpadding=""0"" width=""100%"" bgcolor=""#f2f3f8""
                style=""@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;"">
                <tr>
                    <td>
                        <table style=""background-color: #f2f3f8; max-width:670px;  margin:0 auto;"" width=""100%"" border=""0""
                            align=""center"" cellpadding=""0"" cellspacing=""0"">
                            <tr>
                                <td style=""height:80px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style=""text-align:center;"">
                                    <a href=""http://aqar2023-001-site1.atempurl.com"" title=""logo"" target=""_blank"">
                                        <img width=""60"" src=""http://aqar2023-001-site1.atempurl.com/images/logo.svg"" title=""logo"" alt=""logo"">
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td style=""height:20px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width=""95%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0""
                                        style=""max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);"">
                                        <tr>
                                            <td style=""height:40px;"">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style=""padding:0 35px;"">
                                                <h1 style=""color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;"">لقد طلبت إعادة تعيين كلمة المرور</h1>
                                                <span
                                                    style=""display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;""></span>
                                                <p style=""color:#455056; font-size:15px;line-height:24px; margin:0;"">
                                                   لا يمكننا ببساطة أن نرسل لك كلمة المرور القديمة. تم إنشاء رابط فريد لإعادة تعيين كلمة المرور الخاصة بك. لإعادة تعيين كلمة المرور الخاصة بك ، انقر فوق الارتباط التالي واتبع التعليمات
                                                </p>
                                                <a href=""{resetLink}""
                                                    style=""background:#20e277;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;"">Reset
                                                    Password</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""height:40px;"">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            <tr>
                                <td style=""height:20px;"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style=""text-align:center;"">
                                    <p style=""font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;"">&copy; <strong>عقار 2023</strong></p>
                                </td>
                            </tr>
                            <tr>
                                <td style=""height:80px;"">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <!--/100% body table-->
        </body>
        </html>
    ";
            _emailService.SendEmail(emailDto);

            return true;
        }
        public class RestPAssword
        {

            public string UserId { get; set; }
            [DataType(DataType.Password)]

            public string NewPassword { get; set; }
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Password and confirm password do not match.")]
            public string ConfirmPassword { get; set; }
            public string token { get; set; }


        }

        public async Task<bool> ResetPasswordAsync(RestPAssword restPAssword)
        {
            var user = await _userManager.FindByIdAsync(restPAssword.UserId);
            if (user == null)
            {
                // يمكنك هنا التعامل مع رسالة الخطأ المناسبة إذا كان البريد غير مسجل
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(user, restPAssword.token, restPAssword.NewPassword);
            if (result.Succeeded)
            {
                // يمكنك هنا إجراءات إضافية بمجرد إعادة تعيين كلمة المرور بنجاح
                return true;
            }

            // يمكنك هنا التعامل مع أخطاء إعادة تعيين كلمة المرور
            return false;
        }

        public async Task<string> ChangePassword(UserModel cuurentUser, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(cuurentUser.Id);

            if (user == null)
            {
                throw new ServiceValidationException("User not found.");
            }
            var passwordHasher = new PasswordHasher<AppUser>();
            var passwordResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, changePasswordDto.oldPassword);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new ServiceValidationException("Old password is incorrect.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, changePasswordDto.newPassword);

            if (!result.Succeeded) throw new ServiceValidationException("Password change failed.");

            return "Password changed successfully.";

        }

        public async Task<string> UpdateOfficeOwner(UserModel cuurentUser, UpdateOfficeOwnerDto updateOfficeOwnerDto)
        {
            var userDb = await _userManager.FindByIdAsync(cuurentUser.Id);
            if (userDb == null) throw new ServiceValidationException("المستخدم غير موجود");

            userDb.Email = updateOfficeOwnerDto.Email;
            userDb.FirstName = updateOfficeOwnerDto.FirstName;
            userDb.LastName = updateOfficeOwnerDto.LastName;
            userDb.PhoneNumber = updateOfficeOwnerDto.PhoneNumber;
            userDb.OfficeName = updateOfficeOwnerDto.OfficeName;

            if (updateOfficeOwnerDto.UserImage != null)
            {
                userDb.UserImage = await _imageService.SaveImage(updateOfficeOwnerDto.UserImage, "Images");
            }

            var  result = await _userManager.UpdateAsync(userDb);
            if (!result.Succeeded)
                throw new ServiceValidationException("لم تتم عملية التعديل");

            return "تم تعديل بياناتك بنجاح";
        }

        public async Task<string> UpdateCustomer(UserModel cuurentUser, UpdateCustomerrDto updateCustomerrDto)
        {
            var userDb = await _userManager.FindByIdAsync(cuurentUser.Id);
            if (userDb == null) throw new ServiceValidationException("المستخدم غير موجود");

            userDb.Email = updateCustomerrDto.Email;
            userDb.FirstName = updateCustomerrDto.FirstName;
            userDb.LastName = updateCustomerrDto.LastName;
            userDb.PhoneNumber = updateCustomerrDto.PhoneNumber;
 
            if (updateCustomerrDto.UserImage != null)
            {
                userDb.UserImage = await _imageService.SaveImage(updateCustomerrDto.UserImage, "Images");
            }

            var result = await _userManager.UpdateAsync(userDb);
            if (!result.Succeeded)
                throw new ServiceValidationException("لم تتم عملية التعديل");

            return "تم تعديل بياناتك بنجاح";
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



        public async Task<GetUserProfileDto> GetUserProfil(UserModel cuurentUser)
        {
            var getInfo = await _context.Users.Select(x => new GetUserProfileDto
            {
                Id =x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                OfficeName = x.OfficeName,
                UserImage = x.UserImage,
                Email = x.Email

            }).FirstOrDefaultAsync(x => x.Id == cuurentUser.Id);

            return getInfo;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var dbUseer = await _userManager.FindByIdAsync(userId);

            if (dbUseer == null) throw new ServiceValidationException("المستخد غير موجود");

            dbUseer.IsActive = false;
            await _userManager.UpdateAsync(dbUseer);
            return true;
        }


    }
}