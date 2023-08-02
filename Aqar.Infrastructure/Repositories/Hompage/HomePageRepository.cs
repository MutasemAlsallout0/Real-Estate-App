using Aqar.Core.DTOS.Estate;
using Aqar.Core.DTOS.HomePage;
using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Infrastructure.Extensions;
using AutoMapper;
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
            var data = await _context.Estates.ToListAsync();

            var bulid=  data
                .Where(x => x.EstateType == EstateType.Building)
                .OrderBy(content => content.CreateAt)
                .Take(5)
                .Select(content => _mapper.Map<EstateDTO>(content))
                .ToList();

            var appartment = data
            .Where(x => x.EstateType == EstateType.Appartment)
            .OrderBy(content => content.CreateAt)
            .Take(5)
            .Select(content => _mapper.Map<EstateDTO>(content))
            .ToList();

        var warehouse = data
          .Where(x => x.EstateType == EstateType.Warehouse)
          .OrderBy(content => content.CreateAt)
          .Take(5)
          .Select(content => _mapper.Map<EstateDTO>(content))
          .ToList();


            var land = data
              .Where(x => x.EstateType == EstateType.Land)
              .OrderBy(content => content.CreateAt)
              .Take(5)
              .Select(content => _mapper.Map<EstateDTO>(content))
              .ToList();

            var chalet = data
              .Where(x => x.EstateType == EstateType.Chalet)
              .OrderBy(content => content.CreateAt)
              .Take(5)
              .Select(content => _mapper.Map<EstateDTO>(content))
              .ToList();

            return new EstateData{
                BuildingEstate=bulid,
                AppartmentEstate=appartment,
                WarehouseEstate=warehouse,  
                LandEstate=land,
                ChaletEstate=chalet,

            };
        }

        public async Task<PaginatedList<GetEstateDto>> GetContentsAsync(SearchDto searchDto)
        {


            var content = _context.Estates
                                        .Include(x => x.Street)
                                            .ThenInclude(x => x.City)
                                                .ThenInclude(x => x.Country)
                                                .OrderByDescending(a => a.CreateAt)
                                                .Where(est => est.SeenByAdmin == true).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchDto.street))
            {
                content = content.Where(x => x.Street.Name.ToLower().Contains(searchDto.street.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(searchDto.city))
            {
                content = content.Where(x => x.Street.City.Name.ToLower().Contains(searchDto.city.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(searchDto.country))
            {
                content = content.Where(x => x.Street.City.Country.Name.ToLower().Contains(searchDto.country.ToLower()));
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
