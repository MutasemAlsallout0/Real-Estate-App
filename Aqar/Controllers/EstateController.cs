using Aqar.Core.DTOS.Estate;
using Aqar.Core.Helpers;
using Aqar.Infrastructure.Repositories.Estate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aqar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateController : ControllerBase
    {
        private readonly IEstateRepository _estateRepository;
        public EstateController(IEstateRepository estateRepository) 
        {
            _estateRepository = estateRepository;
        
        }

        [HttpGet]
        public async Task<IActionResult>GetAllEstate([FromQuery] int pageNumber, int pageSize)
        {
            return Ok( await _estateRepository.GetPaginatedDataAsync(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateEstateDTO createEstate)
        {
            return Ok(await _estateRepository.Create(createEstate));
        }
    }
}
