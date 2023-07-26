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
        [Route("api/account/signUpOfficeOwner")]
        public async Task<IActionResult> SignUpOfficeOwner([FromForm] OfficeOwnerRegisterRequest model)
        {
            return Ok(await _authRepository.RegisterOfficeOwner(model));
        }

        [HttpPost]
        [Route("api/account/signUpCustomer")]
        public async Task<IActionResult> SignUpCustomer([FromForm]CustomerRegisterRequest model)
        {
            return Ok(await _authRepository.RegisterCustomer(model));
        }

        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(await _authRepository.Login(request));

        }

        [HttpPost]
        [Route("api/account/forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ResetPasswordRequest model)
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

        [HttpPost]
        [Route("api/account/confirmPasswordReset")]
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

        [HttpPost]
        [Route("api/account/updateOfficeOwner")]
        public async Task<IActionResult> UpdateOfficeOwner([FromForm] UpdateOfficeOwnerDto updateOfficeOwnerDto)
        {
            return Ok(await _authRepository.UpdateOfficeOwner(updateOfficeOwnerDto));

        }
        [HttpPost]
        [Route("api/account/updateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromForm] UpdateCustomerrDto updateCustomerrDto)
        {
            return Ok(await _authRepository.UpdateCustomer(updateCustomerrDto));

        }

        [HttpDelete]
        [Route("api/account/deleteUser")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            return Ok(await _authRepository.DeleteUser(userId));

        }
    }
}
