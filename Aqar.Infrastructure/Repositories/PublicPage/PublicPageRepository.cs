using Aqar.Core.DTOS.ApiBase;
using Aqar.Core.DTOS.Auth.Response;
using Aqar.Core.DTOS.Estate;
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

        public async Task<PagedResult<GetUserProfileDto>> SearchOfficesByNameAsync(string? officeName, int page, int pageSize)
        {
            IQueryable<AppUser> query = _context.Users.Where(u => u.UserType == UserType.OfficeOwner).AsQueryable();

            if(officeName != null)
            {
                query = query.Where(u =>  u.OfficeName.Contains(officeName));
            }

            int totalCount = await query.CountAsync();
            var offices = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(x => new GetUserProfileDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                OfficeName = x.OfficeName,
                UserImage = x.UserImage,
                Email = x.Email,
 

            }).ToListAsync();

            return new PagedResult<GetUserProfileDto>(offices, totalCount);
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



        public async Task<List<GetEstateDto>> GetEstateseDetailsForUserLogin(UserModel currentUser)
        {
            var estatesWithOfficeDetails =await _context.Estates.Include(z => z.User)
                .Include(x => x.Street).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Where(e => e.UserId == currentUser.Id)
                .Select(x => new GetEstateDto
                {
                    Id = x.Id,
                    OwnerEstate = x.User.GetFullName(),
                    UserImage = x.User.UserImage,
                    EstateType = x.DisplayEstateType,
                    ContractType = x.DisplayContractType,
                    Price = x.Price,
                    Area = x.Area,
                    Description = x.Description,
                    street = x.Street.Name,
                    city = x.Street.City.Name,
                    country = x.Street.City.Country.Name,
                    MainImage = x.MainImage,
                    CreateAt = x.CreateAt,
                    IsOwner = true,
                    UserType = x.User.UserType.ToString()
                })
                .ToListAsync();

            return estatesWithOfficeDetails;
        }

        public async Task<string> FollowPublicPage(UserModel currentUser, int publicPageId)
        {
            var existingFollow = await _context.Followings
                .FirstOrDefaultAsync(f => f.UserId == currentUser.Id && f.PublicPageId == publicPageId);

            if (existingFollow != null) throw new ServiceValidationException("انت متابع الصفحة مسبقا");

            var following = new Following
                {
                    UserId = currentUser.Id,
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

        public async Task<string> UnFollowPublicPage(UserModel currentUser)
        {
            var existingFollow = await _context.Followings.FirstOrDefaultAsync(x => x.UserId == currentUser.Id);

            if (existingFollow == null) throw new ServiceValidationException("انت غير متابع الصفحة");

            var publicPage = await _context.PublicPages.FindAsync(existingFollow.PublicPageId);
            publicPage.FollowersCount -= 1;

            _context.Followings.Remove(existingFollow);
            await _context.SaveChangesAsync();

            return "الغاء المتابعة";


        }

        public async Task<GetEsataesForUserDto> GetEstatesForAnyUser(string UserId)
        {
            var estatesWithOfficeDetails = await _context.Estates.Include(z => z.User)
                .Include(x => x.Street).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Where(e => e.UserId == UserId)
                .ToListAsync();

            var estateDetailsList = new List<GetEstateDto>();

            var followersCount = await _context.PublicPages
                             .CountAsync(pf => pf.UserId == UserId);
            var userDetails = estatesWithOfficeDetails.Select(x =>  new GetUserDto
            {
                Id = x.User.Id,
                FullName = x.User.GetFullName(),
                UserImage = x.User.UserImage,
                UserType = x.User.UserType.ToString(),
                FollowersCount = followersCount
            }).FirstOrDefault();

            foreach (var x in estatesWithOfficeDetails)
            {

                var estateDetails = new GetEstateDto
                {
                    Id = x.Id,
                    OwnerEstate = x.User.GetFullName(),
                    UserImage = x.User.UserImage,
                    EstateType = x.DisplayEstateType,
                    ContractType = x.DisplayContractType,
                    Price = x.Price,
                    Area = x.Area,
                    Description = x.Description,
                    street = x.Street.Name,
                    city = x.Street.City.Name,
                    country = x.Street.City.Country.Name,
                    MainImage = x.MainImage,
                    CreateAt = x.CreateAt,
                    IsOwner = true,
                    UserType = x.User.UserType.ToString(),
                 };

                estateDetailsList.Add(estateDetails);
            }
            var data = new GetEsataesForUserDto
            {
                GetUserInfo = userDetails,
                GetEstates = estateDetailsList
            };
            return data;
        }



    }
}