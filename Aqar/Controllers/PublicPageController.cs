using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Repositories.PublicPage;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Aqar.Infrastructure.Repositories.PublicPage.PublicPageRepository;

namespace Aqar.Controllers
{
    [ApiController]
    
    public class PublicPageController : ControllerBase
    {
        private readonly IPublicPageRepository _publicPageRepository;
        public PublicPageController(IPublicPageRepository publicPageRepository)
        {
            _publicPageRepository = publicPageRepository;
        }
        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<AppUser>>> SearchOffices(string?officeName, int page = 1, int pageSize = 10)
        {
            var result = await _publicPageRepository.SearchOfficesByNameAsync(officeName, page, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/publicPage/getEstatesWithOfficeDetails/{userId}")]
        public async Task<IActionResult> GetEstatesWithOfficeDetails(string userId)
        {
            var estatesWithOfficeDetails = await _publicPageRepository.GetEstatesWithOfficeDetailsForUser(userId);

            return Ok(estatesWithOfficeDetails);
        }

        [HttpPost]
        [Route("api/publicPage/followPublicPage")]
        [Authorize]
        public async Task<IActionResult> FollowPublicPage(string userId, int publicPageId)
        {
            //var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok(await _publicPageRepository.FollowPublicPage(userId, publicPageId));
        }

        [HttpPost]
        [Route("api/publicPage/UnFollowPublicPage")]
        [Authorize]
        public async Task<IActionResult> UnFollowPublicPage(string userId)
        {
       
            return Ok(await _publicPageRepository.UnFollowPublicPage(userId));
        }
    }
}
