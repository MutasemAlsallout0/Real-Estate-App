using Aqar.Core.DTOS.Estate;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Exceptions;
using Aqar.Infrastructure.Extensions;
using Aqar.Infrastructure.HelperServices.ImageHelper;
using AutoMapper;
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
            var query = _context.Estates.AsQueryable().Where(x => x.SeenByAdmin == true);
            var totalCount = await query.CountAsync();
            var pageData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var itemes = await PaginatedList<Aqar.Data.Model.Estate>.CreateAsync(query, pageNumber, pageSize);
            return itemes;
        }

        public async Task<GetEstateDto> GetEstate(int Id)
        {
            var estate = await _context.Estates
                .Include(z => z.User)
                .Include(x => x.Street).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == Id && x.SeenByAdmin == true);

            if (estate == null) 
                throw new ServiceValidationException("لا يوجد عقار!");
            
                var imagesUrls = await _context.Attachments
                    .Where(ei => ei.EstateId == estate.Id)
                    .Select(ei => ei.Image)
                    .ToListAsync();

                var estateDTO = new GetEstateDto
                {
                    Id = estate.Id,
                    Images = imagesUrls,
                    Description = estate.Description,
                    Price = estate.Price,
                    EstateType = estate.EstateType.ToString(),
                    ContractType = estate.ContractType.ToString(),
                    street = estate.Street.Name,
                    city = estate.Street.City.Name,
                    country = estate.Street.City.Country.Name,
                    OwnerEstate = estate.User.GetFullName(),
                    Area = estate.Area

                };

                return estateDTO;                    
        }


        public async Task<List<Attachment>> AddAttachment(List<Attachment> attachments)
        {
            await _context.Attachments.AddRangeAsync(attachments);
            await _context.SaveChangesAsync();
            return attachments;
        }


        public async Task<Data.Model.Estate> Create( CreateEstateDTO input)
        {

            var estate = _mapper.Map<Data.Model.Estate>(input);

            estate.SeenByAdmin = false;

            await _context.Estates.AddAsync(estate);
            await _context.SaveChangesAsync();

            return estate;
           
        }

        public async Task<Data.Model.Estate> UpdateEstate(UpdateEstateDTO input)
        {
            var dbestate = await _context.Estates.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (dbestate == null) throw new ServiceValidationException("لا يوجد عقار!");

            dbestate.EstateType = input.EstateType;
            dbestate.ContractType = input.ContractType;
            dbestate.Price = input.Price;
            dbestate.Description = input.Description;
            dbestate.Area = input.Area;
            dbestate.StreetId = input.StreetId;

            var Result =  _context.Estates.Update(dbestate);

            await _context.SaveChangesAsync();

            return dbestate;

        }

        public async Task<string> DeleteEstate(int id)
        {
            var dbestate = await _context.Estates.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);

            if (dbestate == null) throw new ServiceValidationException("لا يوجد عقار!");

             foreach (var estateImage in dbestate.Images.ToList())
            {
                var dbestateAttachment = await _context.Attachments.FirstOrDefaultAsync(x => x.Id == estateImage.Id);
                if (dbestateAttachment != null)
                {
                    _context.Attachments.Remove(dbestateAttachment);
                }
            }

             _context.Estates.Remove(dbestate);

            await _context.SaveChangesAsync();

            return "تم حذف العقار والمرفقات المرتبطة بنجاح!";
        }


    }
}