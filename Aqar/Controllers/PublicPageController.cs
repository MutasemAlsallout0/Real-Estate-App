using Aqar.Data.Model;
using Aqar.Infrastructure.Repositories.PublicPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Aqar.Infrastructure.Repositories.PublicPage.PublicPageRepository;

namespace Aqar.Controllers
{
    [ApiController]
    
    public class PublicPageController : ApiBaseController
    {
        private readonly IPublicPageRepository _publicPageRepository;
        public PublicPageController(IPublicPageRepository publicPageRepository)
        {
            _publicPageRepository = publicPageRepository;
        }
        [HttpGet]
        [Route("api/publicPage/searchOffices")]
        public async Task<ActionResult<PagedResult<AppUser>>> SearchOffices(string?officeName, int page = 1, int pageSize = 10)
        {
            var result = await _publicPageRepository.SearchOfficesByNameAsync(officeName, page, pageSize);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/publicPage/getEstateseDetailsForUserLogin")]
        public async Task<IActionResult> GetEstateseDetailsForUserLogin()
        {
            var estatesWithOfficeDetails = await _publicPageRepository.GetEstateseDetailsForUserLogin(LoggedInUser);

            return Ok(estatesWithOfficeDetails);
        }

        [HttpPost]
        [Route("api/publicPage/followPublicPage")]
        [Authorize]
        public async Task<IActionResult> FollowPublicPage(int publicPageId)
        {

            return Ok(await _publicPageRepository.FollowPublicPage(LoggedInUser, publicPageId));
        }

        [HttpPost]
        [Route("api/publicPage/UnFollowPublicPage")]
        [Authorize]
        public async Task<IActionResult> UnFollowPublicPage()
        {
       
            return Ok(await _publicPageRepository.UnFollowPublicPage(LoggedInUser));
        }
    }
}
