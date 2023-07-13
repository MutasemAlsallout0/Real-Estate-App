using Aqar.Core.DTOS.Auth.Request;
using Aqar.Core.DTOS.Auth.Response;
using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Exceptions;
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
        //private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly AqarDbContext _context;
        public AuthRepository(IConfiguration configuration, UserManager<AppUser> userManager,
            IMapper mapper, AqarDbContext context)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
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


            var Expiration = DateTime.Now.AddHours(1);


            // token 
            var token = new JwtSecurityToken(
               issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
               claims: claims,
               expires: Expiration,
               signingCredentials: cred);

            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = Expiration
            };
        }

     

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