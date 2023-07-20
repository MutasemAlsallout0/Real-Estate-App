using Aqar.Core.DTOS.Estate;
using Aqar.Infrastructure.Extensions;

namespace Aqar.Infrastructure.Repositories.Estate
{
    public interface IEstateRepository
    {
        Task<PaginatedList<Data.Model.Estate>> GetPaginatedDataAsync(int pageNumber, int pageSize);
        Task<bool> Create(CreateEstateDTO input);
    }
}