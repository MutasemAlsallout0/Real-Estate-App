using Aqar.Core.DTOS.Estate;
using Aqar.Core.Helpers;
using Aqar.Data.Model;
using Aqar.Infrastructure.HelperServices.ImageHelper;
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
        private readonly IImageService _imageService;
        public EstateController(IEstateRepository estateRepository, IImageService imageService) 
        {
            _estateRepository = estateRepository;
            _imageService = imageService;
        
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult>GetAllEstate([FromQuery] int pageNumber, int pageSize)
        {
            return Ok( await _estateRepository.GetPaginatedDataAsync(pageNumber, pageSize));
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetEstate(int Id)
        {
            return Ok(await _estateRepository.GetEstate(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateEstateDTO input)
        {
           var result= await _estateRepository.Create(input);

            if (input.Imagesfile != null)
            {
                List<Attachment> attachments = new List<Attachment>();

                foreach (var item in input.Imagesfile)
                {
                    var name = await _imageService.UploadImageAsync(item);
                    attachments.Add(new Attachment
                    {
                        EstateId = result.Id,
                        Image = name
                    });
                }
                await _estateRepository.AddAttachment(attachments);


                // await AddAttachment(attachments);
            }
            return Ok("تم اضافة عقار جديد");
        }



        [HttpPut]
        public async Task<IActionResult> UpdateEstate([FromForm] UpdateEstateDTO input)
        {
            var result = await _estateRepository.UpdateEstate(input);

            if (input.Imagesfile != null)
            {
                List<Attachment> attachments = new List<Attachment>();

                foreach (var item in input.Imagesfile)
                {
                    var name = await _imageService.UploadImageAsync(item);
                    attachments.Add(new Attachment
                    {
                        EstateId = result.Id,
                        Image = name
                    });
                }
                await _estateRepository.AddAttachment(attachments);


                // await AddAttachment(attachments);
            }
            return Ok("تم تعديل العقار ");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEstate(int id)
        {
            return Ok(await _estateRepository.DeleteEstate(id));
        }
    }
}
