using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.Exceptions;
using Aqar.Infrastructure.HelperServices.ImageHelper;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aqar.Infrastructure.Repositories.PublicPage
{
    public class PublicPageRepository : IPublicPageRepository
    {
        private readonly AqarDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public PublicPageRepository(AqarDbContext context,
            IMapper mapper,
            IImageService imageService,
            IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        //var publicPage = await CreatePublicPageForOfficeOwner(user.Id, model.OfficeName);

        public async Task<Data.Model.PublicPage> CreatePublicPageForOfficeOwner(string userId, string officeName)
        {
            var officeUser = await _userManager.FindByIdAsync(userId);
            if (officeUser == null)
            {
                // التحقق من أن المستخدم موجود.
                throw new ServiceValidationException("مستخدم غير موجود.");
            }

            var publicPage = new Data.Model.PublicPage
            {
                Name = officeUser.OfficeName,
                ActiveState = true,
                UserId = userId,
                User = officeUser
            };

            _context.PublicPages.Add(publicPage);
            await _context.SaveChangesAsync();

            return publicPage;
        }

        //public async Task<List<Data.Model.Estate>> GetEstatesForUser(string userId)
        //{
        //    // استعلام لاسترداد قائمة العقارات للمستخدم المحدد (من خلال UserId)
        //    var estates =await _context.Estates
        //        .Where(e => e.UserId == userId)
        //        .ToListAsync();

        //    return estates;
        //}
        public class EstateWithOfficeDetails
        {
            public int EstateId { get; set; }
            public EstateType EstateType { get; set; }
            public ContractType ContractType { get; set; }
            public double Price { get; set; }
            public double Area { get; set; }
            public string Description { get; set; } = string.Empty;
            public bool SeenByAdmin { get; set; }
            public List<Attachment> Images { get; set; }
            public string OfficeId { get; set; }
            public string OfficeName { get; set; }
            public string OfficeImage { get; set; }
        }

        public async Task<List<EstateWithOfficeDetails>> GetEstatesWithOfficeDetailsForUser(string userId)
        {
            var estatesWithOfficeDetails =await _context.Estates
                .Where(e => e.UserId == userId)
                .Select(e => new EstateWithOfficeDetails
                {
                    EstateId = e.Id,
                    EstateType = e.EstateType,
                    ContractType = e.ContractType,
                    Price = e.Price,
                    Area = e.Area,
                    Description = e.Description,
                    SeenByAdmin = e.SeenByAdmin,
                    Images = e.Images,
                    OfficeId = e.User.Id,
                    OfficeName = e.User.OfficeName,
                    OfficeImage = e.User.UserImage
                })
                .ToListAsync();

            return estatesWithOfficeDetails;
        }

        //public async Task<PaginatedList<Estate>> GetPaginatedDataAsync(int pageNumber, int pageSize)
        //{
        //    var query = _context.Estates.AsQueryable();
        //    var totalCount = await query.CountAsync();
        //    var pageData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        //    var itemes = await PaginatedList<Estate>.CreateAsync(query, pageNumber, pageSize);
        //    return itemes;
        //}


        //public async Task<List<PublicPageDto>> GetTrainingCompany()
        //{
        //    var publicPages = await _context.PublicPages.ToListAsync();
        //    return _mapper.Map<List<PublicPageDto>>(publicPages);

        //}

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