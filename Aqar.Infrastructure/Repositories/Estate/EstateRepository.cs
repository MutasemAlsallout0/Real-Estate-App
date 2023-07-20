using Aqar.Core.DTOS.Estate;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
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
            var query = _context.Estates.AsQueryable();
            var totalCount = await query.CountAsync();
            var pageData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var itemes = await PaginatedList<Aqar.Data.Model.Estate>.CreateAsync(query, pageNumber, pageSize);
            return itemes;
        }
        //public async Task<List<TrainingCompanyDto>> GetTrainingCompany()
        //{
        //    var training = await _context.Estates.ToListAsync();
        //    return _mapper.Map<List<Estate>>(training);

        //}

        //public async Task<TrainingCompany> Add(CreateTrainingCompanyDto companyDto)
        //{

        //    var training = _mapper.Map<TrainingCompany>(companyDto);
        //    if (companyDto.LogoCompany != null)
        //    {
        //        training.LogoCompany = await _imageService.SaveImage(companyDto.LogoCompany, "Images");
        //    }
        //    await _context.TrainingCompanys.AddAsync(training);
        //    await _context.SaveChangesAsync();
        //    return training;
        //}

        public async Task<bool> Create( CreateEstateDTO input)
        {
            if (input is null) return false;


            var estate = _mapper.Map<Data.Model.Estate>(input);
        //   estate.EstateType=input.EstateType.ToString();
            estate.Images = new List<Attachment>
            {
                new Attachment{Image="test1"},
                new Attachment{Image="test2"}

            };
            var Result = _context.Estates.Add(estate);
          
            
            if (Result.State == Microsoft.EntityFrameworkCore.EntityState.Unchanged || Result.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                _context.SaveChanges();
                return true;
            }

            else return false;
        }

    }
}