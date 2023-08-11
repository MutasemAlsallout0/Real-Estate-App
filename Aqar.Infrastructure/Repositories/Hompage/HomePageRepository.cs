using Aqar.Core.DTOS.Estate;
using Aqar.Core.DTOS.HomePage;
using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Infrastructure.Extensions;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;

namespace Aqar.Infrastructure.Repositories.Hompage
{
    public class HomePageRepository : IHomePageRepository
    {
        private readonly AqarDbContext _context;
        private readonly IMapper _mapper;

        public HomePageRepository(AqarDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HomePageData> AllData()
        {


            return new HomePageData
            {
                GetEstateData = await EstateDataAsync()
        

             };
        }


        public async Task<EstateData> EstateDataAsync()
        {
            var data = await _context.Estates.Include(z => z.User)
                .Include(x => x.Street).ThenInclude(x => x.City).ThenInclude(x => x.Country).ToListAsync();

            var bulid=  data
                .Where(x => x.EstateType == EstateType.Building)
                .OrderBy(content => content.CreateAt)
                .Take(5)
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

                })
                .ToList();

            var appartment = data
            .Where(x => x.EstateType == EstateType.Appartment)
            .OrderBy(content => content.CreateAt)
            .Take(5)
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

                }).ToList();

        var office = data
          .Where(x => x.EstateType == EstateType.Office)
          .OrderBy(content => content.CreateAt)
          .Take(5)
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

                }).ToList();


            var land = data
              .Where(x => x.EstateType == EstateType.Land)
              .OrderBy(content => content.CreateAt)
              .Take(5)
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

                }).ToList();

            var chalet = data
              .Where(x => x.EstateType == EstateType.Chalet)
              .OrderBy(content => content.CreateAt)
              .Take(5)
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

                }).ToList();

            var store = data
              .Where(x => x.EstateType == EstateType.Store)
              .OrderBy(content => content.CreateAt)
              .Take(5)
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

                }).ToList();
            var room = data
              .Where(x => x.EstateType == EstateType.Room)
              .OrderBy(content => content.CreateAt)
              .Take(5)
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

                }).ToList();
            var garage = data
              .Where(x => x.EstateType == EstateType.Garage)
              .OrderBy(content => content.CreateAt)
              .Take(5)
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

                }).ToList();
            return new EstateData{
                BuildingEstate=bulid,
                AppartmentEstate=appartment,
                OfficeEstate=office,  
                LandEstate=land,
                ChaletEstate=chalet,
                StoreEstate=store,
                RoomEstate=room,
                GarageEstate=garage,

            };
        }

        public async Task<PaginatedList<GetEstateDto>> GetContentsAsync(SearchDto searchDto)
        {


            var content = _context.Estates
                                        .Include(x => x.Street)
                                            .ThenInclude(x => x.City)
                                                .ThenInclude(x => x.Country)
                                                .Include(x => x.User)
                                                .OrderByDescending(a => a.CreateAt)
                                                .AsQueryable();

            if (searchDto.streetId != 0)
            {
                content = content.Where(x => x.StreetId == searchDto.streetId);
            }
            if (searchDto.cityId != 0)
            {
                content = content.Where(x => x.CityId == searchDto.cityId);
            }
            if (searchDto.countryId != 0)
            {
                content = content.Where(x => x.CountryId == searchDto.countryId);
            }
            if (searchDto.contractType != 0)
            {
                content = content.Where(x => x.ContractType == searchDto.contractType);
            }
            if (searchDto.estateType != 0)
            {
                content = content.Where(x => x.EstateType == searchDto.estateType);
            }

            var data = await content.Skip((searchDto.pageNumber - 1) * searchDto.pageSize).Take(searchDto.pageSize)
                        .Select(x => new GetEstateDto
                        {
                            Id = x.Id,
                            OwnerEstate = x.User.GetFullName(),
                            UserImage = x.User.UserImage,
                            EstateType = x.EstateType.ToString(),
                            ContractType = x.ContractType.ToString(),
                            Price = x.Price,
                            Area = x.Area,
                            Description = x.Description,
                            street = x.Street.Name,
                            city = x.Street.City.Name,
                            country = x.Street.City.Country.Name,
                            MainImage = x.MainImage,
                            
                         }).ToListAsync();
            var totalCount = await content.CountAsync();


            var paginatedList = new PaginatedList<GetEstateDto>(data, totalCount, searchDto.pageNumber, searchDto.pageSize);

            return paginatedList;
        }
    }
}
