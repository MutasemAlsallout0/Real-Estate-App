using Aqar.Core.DTOS.PublicPage;
using Aqar.Data.DataLayer;
using Aqar.Infrastructure.HelperServices.ImageHelper;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Aqar.Infrastructure.Repositories.PublicPage
{
    public class PublicPageRepository
    {
        private readonly AqarDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PublicPageRepository(AqarDbContext context,
            IMapper mapper,
            IImageService imageService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<PaginatedList<Estate>> GetPaginatedDataAsync(int pageNumber, int pageSize)
        //{
        //    var query = _context.Estates.AsQueryable();
        //    var totalCount = await query.CountAsync();
        //    var pageData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        //    var itemes = await PaginatedList<Estate>.CreateAsync(query, pageNumber, pageSize);
        //    return itemes;
        //}


        public async Task<List<PublicPageDto>> GetTrainingCompany()
        {
            var publicPages = await _context.PublicPages.ToListAsync();
            return _mapper.Map<List<PublicPageDto>>(publicPages);

        }

        //public async Task<PublicPage> Add(PublicPageDto publicPageDto)
        //{

        //    var publicPage = _mapper.Map<PublicPage>(publicPageDto);
        //    if (companyDto.LogoCompany != null)
        //    {
        //        training.LogoCompany = await _imageService.SaveImage(companyDto.LogoCompany, "Images");
        //    }
        //    await _context.PublicPages.AddAsync(training);
        //    await _context.SaveChangesAsync();
        //    return training;
        //}


        //public async Task<TrainingCompany> Update(PublicPageDto publicPageDto)
        //{
        //    var training = await _context.PublicPages.FindAsync(updateTrainingCompany.Id);
        //    _mapper.Map(updateTrainingCompany, training);
        //    if (updateTrainingCompany.NewLogo != null)
        //    {
        //        training.LogoCompany = await _imageService.SaveImage(updateTrainingCompany.NewLogo, "Images");
        //    }
        //    await _context.SaveChangesAsync();
        //    return training;

        //}

        //public async Task<bool> Delete(int id)
        //{
        //    var publicPage = await _context.PublicPages.FindAsync(id);
        //    if (publicPage == null)
        //        return false;
        //    else
        //    {
        //        _context.PublicPages.Remove(publicPage);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //}
    }
}