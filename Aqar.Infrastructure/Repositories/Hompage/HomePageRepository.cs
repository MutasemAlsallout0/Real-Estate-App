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

        public async Task<PaginationContent> GetContentsAsync(string term, EstateType? estateType, ContractType? contractType,   int currentPage)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var contentData = new PaginationContent
            {
             };

            var content = from est in _context.Estates.Include(x=>x.User).Include(x=>x.Images).Include(x=>x.Street).ThenInclude(x=>x.City).ThenInclude(x=>x.Country)
                          where (term == "" || est.Street.City.Name.Contains(term.ToLower())) ||
         (!estateType.HasValue || est.EstateType == estateType) ||
         (!contractType.HasValue || est.ContractType == contractType)

                          select new Data.Model.Estate
                          {
                              Id = est.Id,
                              EstateType = est.EstateType,
                              ContractType = est.ContractType,
                              Price = est.Price,
                              Area = est.Area,
                              Description = est.Description,
                              StreetId = est.StreetId,
                              UserId = est.UserId,
                              Images = est.Images,
                              IsDelete = est.IsDelete,
                          };

          

                
                    content = content.OrderBy(a => a.CreateAt);
                  
            

           // int totalRecords = content.Count();
            int pageSize = 10;
           // int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            content = content.Skip((currentPage - 1) * pageSize).Take(pageSize);

            contentData.GetEstates = await content.ToListAsync();
            contentData.CurrentPage = currentPage;
            //contentData.TotalPages = totalPages;
            contentData.Term = term;
            contentData.PageSize = pageSize;
      //      contentData.OrderBy = orderBy;

            return contentData;
        }


    }
}
