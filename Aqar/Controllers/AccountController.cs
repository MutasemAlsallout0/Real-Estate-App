using Aqar.Core.DTOS.Auth.Request;
using Aqar.Infrastructure.Managers.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Aqar.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AccountController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(await _authRepository.Login(request));

        }


        [HttpPost]
        [Route("api/account/signUp")]
        public async Task<IActionResult> SignUp(RegisterRequest model)
        {
            return Ok(await _authRepository.RegisterUserAsync(model));
        }
    }
}
