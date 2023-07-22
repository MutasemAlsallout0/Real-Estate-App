using Aqar.Core.DTOS.Estate;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Extensions;
using Aqar.Infrastructure.HelperServices.ImageHelper;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
 

namespace Aqar.Infrastructure.Repositories.Estate
{
    public class EstateRepository : IEstateRepository
    {
        private readonly AqarDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EstateRepository(AqarDbContext context,
            IMapper mapper,
            IImageService imageService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PaginatedList<Aqar.Data.Model.Estate>> GetPaginatedDataAsync(int pageNumber, int pageSize)
        {
            var query = _context.Estates.AsQueryable();
            var totalCount = await query.CountAsync();
            var pageData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var itemes = await PaginatedList<Aqar.Data.Model.Estate>.CreateAsync(query, pageNumber, pageSize);
            return itemes;
        }
        public async Task<List<Attachment>> AddAttachment(List<Attachment> attachments)
        {
            await _context.Attachments.AddRangeAsync(attachments);
            await _context.SaveChangesAsync();
            return attachments;
        }


        public async Task<bool> Create( CreateEstateDTO input)
        {
            if (input is null) return false;


            var estate = _mapper.Map<Data.Model.Estate>(input);
            ////   estate.EstateType=input.EstateType.ToString();
            ///
            var Result = _context.Estates.Add(estate);
            if (input.Imagesfile != null)
            {
                List<Attachment> attachments = new List<Attachment>();

                foreach (var item in input.Imagesfile)
                {
                    var name = await _imageService.UploadImageAsync(item);
                    attachments.Add(new Attachment
                    {
                        // NewsContentId = entity.Id,
                        EstateId=estate.Id,
                        Image = name
                    });
                }
                await AddAttachment(attachments);


                // await AddAttachment(attachments);
            }
            //foreach (var item in input.Imagesfile)
            //{


            //    var name = await _imageService.UploadImageAsync(item);
            //    estate.Images = new List<Attachment>();
            //    estate.Images.Add(new Attachment
            //    {
            //        //   NewsContentId = newsContent.Id,
            //        Image = name
            //    });
            //    await _context.SaveChangesAsync();
            //}


            //estate.Images = new List<Attachment>
            //{
            //    new Attachment{Image="test1"},
            //    new Attachment{Image="test2"}

            //};




            _context.SaveChanges();
                return true;
            

           
        }

    }
}