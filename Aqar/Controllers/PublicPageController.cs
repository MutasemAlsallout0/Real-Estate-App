using Aqar.Infrastructure.Repositories.PublicPage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Aqar.Infrastructure.Repositories.PublicPage.PublicPageRepository;

namespace Aqar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicPageController : ControllerBase
    {
        private readonly IPublicPageRepository _publicPageRepository;
        public PublicPageController(IPublicPageRepository publicPageRepository)
        {
            _publicPageRepository = publicPageRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult>GetEstatesWithOfficeDetails(string userId)
        {
            var estatesWithOfficeDetails = await _publicPageRepository.GetEstatesWithOfficeDetailsForUser(userId);

            return Ok(estatesWithOfficeDetails);
        }
    }
}
