using Aqar.Core.DTOS.HomePage;
using Aqar.Core.Enums;
using Aqar.Infrastructure.Extensions;

namespace Aqar.Infrastructure.Repositories.Hompage
{
    public interface IHomePageRepository
    {
        // Task<PaginationContent> GetContentsAsync(string term, EstateType? estateType, ContractType? contractType, string orderBy, int currentPage);

        Task<PaginationContent> GetContentsAsync(string term, EstateType? estateType, ContractType? contractType, int currentPage);
        Task<HomePageData> AllData();
    }
}