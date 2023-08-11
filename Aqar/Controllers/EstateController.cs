using Aqar.Core.DTOS.Estate;
using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.HelperServices.ImageHelper;
using Aqar.Infrastructure.Repositories.Estate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aqar.Controllers
{
    [ApiController]
    [Authorize]
    public class EstateController : ApiBaseController
    {
        private readonly IEstateRepository _estateRepository;
        private readonly IImageService _imageService;
        public EstateController(IEstateRepository estateRepository, IImageService imageService) 
        {
            _estateRepository = estateRepository;
            _imageService = imageService;
        
        }


        [HttpGet]
        [Route("api/estate/getCountries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _estateRepository.GetCountries());
        }

        [HttpGet]
        [Route("api/estate/getCities/{countryId}")]
        public async Task<IActionResult> GetCities(int countryId)
        {
            return Ok(await _estateRepository.GetCities(countryId));
        }


        [HttpGet]
        [Route("api/estate/getAllEstate")]
        public async Task<IActionResult> GetAllEstate([FromQuery] int pageNumber, int pageSize, EstateType? estateType)
        {
            return Ok( await _estateRepository.GetPaginatedDataAsync(pageNumber, pageSize,estateType));
        }

        [HttpGet]
        [Route("api/estate/getEstate/{Id}")]
        public async Task<IActionResult> GetEstate(int Id)
        {
            return Ok(await _estateRepository.GetEstate(LoggedInUser, Id));
        }

        [HttpPost]
        [Route("api/estate/addEstate")]
        public async Task<IActionResult> AddEstate([FromForm]CreateEstateDTO input)
        {
           var result= await _estateRepository.Create(LoggedInUser, input);

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
            }
            return Ok("تم اضافة العقار بنجاح");
        }



        [HttpPut]
        [Route("api/estate/updateEstate")]
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
            }
            return Ok("تم تعديل العقار ");
        }

        [HttpDelete]
        [Route("api/estate/deleteEstate/{id}")]
        public async Task<IActionResult> DeleteEstate(int id)
        {
            return Ok(await _estateRepository.DeleteEstate(id));
        }
    }
}
