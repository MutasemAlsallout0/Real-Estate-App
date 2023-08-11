using Aqar.Data.DataLayer;
using Aqar.Infrastructure.Repositories.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aqar.Controllers
{
    [ApiController]
    [Authorize(Roles = RolesName.Admin)]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet]
        [Route("api/admin/pendingEstates")]
        public async Task<IActionResult> GetPendingEstates()
        {
            var pendingEstates = await _adminRepository.GetPendingEstates();
            return Ok(pendingEstates);
        }

        [HttpPost]
        [Route("api/admin/approveEstate/{estateId}")]
        public async Task<IActionResult> ApproveEstate(int estateId)
        {
            var approveEstate =  await _adminRepository.ApproveEstate(estateId);
            return Ok(approveEstate);
        }
    }
}
