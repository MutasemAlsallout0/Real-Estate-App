using Aqar.Core.DTOS.Estate;
using Aqar.Core.DTOS.HomePage;
using Aqar.Core.Enums;
using Aqar.Infrastructure.Extensions;

namespace Aqar.Infrastructure.Repositories.Hompage
{
    public interface IHomePageRepository
    {
        // Task<PaginationContent> GetContentsAsync(string term, EstateType? estateType, ContractType? contractType, string orderBy, int currentPage);
       // Task<PaginationContent> GetContentsAsync(string term, string orderBy, int currentPage, EstateType estateType, ContractType contractType);
        //Task<PaginationContent> GetContentsAsync(string term, EstateType? estateType, ContractType? contractType, int currentPage);
        Task<HomePageData> AllData();
        Task<PaginatedList<GetEstateDto>> GetContentsAsync(SearchDto searchDto);


    }
}