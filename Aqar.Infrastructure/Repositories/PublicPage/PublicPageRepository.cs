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

        public class PagedResult<T>
        {
            public List<T> Data { get; set; }
            public int TotalCount { get; set; }

            public PagedResult(List<T> data, int totalCount)
            {
                Data = data;
                TotalCount = totalCount;
            }
        }

        public async Task<PagedResult<AppUser>> SearchOfficesByNameAsync(string? officeName, int page, int pageSize)
        {
            IQueryable<AppUser> query = _context.Users
                .Where(u => u.UserType == UserType.OfficeOwner && u.OfficeName.Contains(officeName));

            int totalCount = await query.CountAsync();
            List<AppUser> offices = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<AppUser>(offices, totalCount);
        }

        public async Task<Data.Model.PublicPage> CreatePublicPageForOfficeOwner(string userId)
        {
            var officeUser = await _userManager.FindByIdAsync(userId);
            if (officeUser == null)
            {
                throw new ServiceValidationException("مستخدم غير موجود.");
            }

            var publicPage = new Data.Model.PublicPage
            {
                Name = officeUser.OfficeName,
                UserId = userId,
             };

            _context.PublicPages.Add(publicPage);
            await _context.SaveChangesAsync();

            return publicPage;
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

        public async Task<string> FollowPublicPage(string userId, int publicPageId)
        {
            var existingFollow = await _context.Followings
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PublicPageId == publicPageId);

            if (existingFollow != null) throw new ServiceValidationException("انت متابع الصفحة مسبقا");

            var following = new Following
                {
                    UserId = userId,
                    PublicPageId = publicPageId,
                    IsFollow = true
                };

                _context.Followings.Add(following);
                await _context.SaveChangesAsync();

                var publicPage = await _context.PublicPages.FindAsync(publicPageId);
                publicPage.FollowersCount += 1;
                await _context.SaveChangesAsync();
                return "متابعة";
            

         }

        public async Task<string> UnFollowPublicPage(string userId)
        {
            var existingFollow = await _context.Followings.FirstOrDefaultAsync(x => x.UserId == userId);

            if (existingFollow == null) throw new ServiceValidationException("انت غير متابع الصفحة");

            var publicPage = await _context.PublicPages.FindAsync(existingFollow.PublicPageId);
            publicPage.FollowersCount -= 1;

            _context.Followings.Remove(existingFollow);
            await _context.SaveChangesAsync();

            return "الغاء المتابعة";


        }
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





    }
}