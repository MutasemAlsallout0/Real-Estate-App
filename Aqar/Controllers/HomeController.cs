using Aqar.Core.DTOS.HomePage;
using Aqar.Infrastructure.Repositories.Hompage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Aqar.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomePageRepository _homePageRepository;

        public HomeController(IHomePageRepository homePageRepository)
        {
            _homePageRepository = homePageRepository;
        }


        [HttpGet]
        [Route("api/mainHome/allData")]
        public async Task<IActionResult> AllData()
        {
            return Ok(await _homePageRepository.AllData());
        }

        [HttpPost]
        [Route("api/mainHome/getContentsSearch")]
        public async Task<IActionResult> GetContents( SearchDto searchDto) 
        {
            
               
                var contents = await _homePageRepository.GetContentsAsync(searchDto);
                return Ok(contents); 
            
        }
    }
}
