using Aqar.Core.DTOS.ApiBase;
using Aqar.Infrastructure.Exceptions;
using Aqar.Infrastructure.Repositories.CommonRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace Aqar.Controllers
{
    public class ApiBaseController : Controller
    {
        private UserModel _loggedInUser;

        protected UserModel LoggedInUser
        {
            get
            {
                if (_loggedInUser != null) return LoggedInUser;

                Request.Headers.TryGetValue("Authorization", out StringValues Token);

                if (string.IsNullOrEmpty(Token))
                {
                    _loggedInUser = null;
                    return _loggedInUser;
                }
                var cliamId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (cliamId == null)
                {
                    throw new ServiceValidationException(401, "Invalid or expire token");
                }

                var commonManager = HttpContext.RequestServices.GetService(typeof(ICommonRepository)) as ICommonRepository;
                _loggedInUser = commonManager.GetUserRole(new UserModel { Id = cliamId.Value });
                return _loggedInUser;
            }
        }
        public ApiBaseController()
        {

        }
    }
}
