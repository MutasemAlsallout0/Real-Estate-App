using Aqar.Core.Enums;
using Aqar.Infrastructure.Extensions;
using Aqar.Infrastructure.Repositories.Hompage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Aqar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomePageRepository _homePageRepository;

        public HomeController(IHomePageRepository homePageRepository)
        {
            _homePageRepository = homePageRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var allAboutUs = ;
            return Ok(await _homePageRepository.AllData());
        }
        [HttpGet("pagenation")]
        public async Task<ActionResult<PaginationContent>> GetContents(
     string term,
     EstateType? estateType,
     ContractType? contractType,
     int currentPage
 )
        {
            try
            {
                // استدعاء الدالة GetContentsAsync وتمرير المعايير للبحث
                var contents = await _homePageRepository.GetContentsAsync(term, estateType, contractType, currentPage);
                return Ok(contents); // الاستجابة تحتوي على المحتوى المرتجع من الدالة GetContentsAsync
            }
            catch (Exception ex)
            {
                // يمكنك التعامل مع الأخطاء هنا وإرجاع استجابة خاصة بالخطأ إذا لزم الأمر.
                return StatusCode(500, "حدث خطأ أثناء معالجة الطلب.");
            }
        }
    }
}
