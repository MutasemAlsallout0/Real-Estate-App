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


        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.SendResetPasswordEmailAsync(model.Email);

            if (result)
            {
                return Ok(new { Message = "تم إرسال رابط إعادة تعيين كلمة المرور بنجاح. يرجى التحقق من بريدك الإلكتروني." });
            }

            return BadRequest(new { Message = "تعذر إرسال رابط إعادة تعيين كلمة المرور. يُرجى التحقق من صحة البريد الإلكتروني والتأكد من أنه مؤكد." });
        }

        [HttpPost("confirmPasswordReset")]
        public async Task<IActionResult> ConfirmPasswordReset(ConfirmPasswordResetRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);

            if (result)
            {
                return Ok(new { Message = "تم إعادة تعيين كلمة المرور بنجاح." });
            }

            return BadRequest(new { Message = "فشل في إعادة تعيين كلمة المرور. يُرجى التحقق من صحة الرابط وإدخال كلمة مرور جديدة صحيحة." });
        }
    }
}
