using Aqar.Core.DTOS.Auth.Request;
using Aqar.Data.DataLayer;
using Aqar.Infrastructure.Managers.Auth;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Aqar.Infrastructure.Managers.Auth.AuthRepository;

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
        public async Task<IActionResult> ConfirmPasswordReset(RestPAssword model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.ResetPasswordAsync(model);

            if (result)
            {
                return Ok(new { Message = "تم إعادة تعيين كلمة المرور بنجاح." });
            }

            return BadRequest(new { Message = "فشل في إعادة تعيين كلمة المرور. يُرجى التحقق من صحة الرابط وإدخال كلمة مرور جديدة صحيحة." });
        }

        [HttpPut]
        [Route("api/account/updateOfficeOwner")]
        [Authorize(Roles = RolesName.OfficeOwner)]
        public async Task<IActionResult> UpdateOfficeOwner([FromForm] UpdateOfficeOwnerDto updateOfficeOwnerDto)
        {
            return Ok(await _authRepository.UpdateOfficeOwner(updateOfficeOwnerDto));

        }

        [HttpPut]
        [Route("api/account/updateCustomer")]
        [HttpPut]
        [Authorize(Roles = RolesName.Customer)]
        public async Task<IActionResult> UpdateCustomer([FromForm] UpdateCustomerrDto updateCustomerrDto)
        {
            return Ok(await _authRepository.UpdateCustomer(updateCustomerrDto));

        }

        [HttpDelete]
        [Route("api/account/deleteAccount")]

        public async Task<IActionResult> DeleteAccount(string userId)
        {
            return Ok(await _authRepository.DeleteUser(userId));

        }
    }
}
