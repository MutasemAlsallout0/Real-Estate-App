using Aqar.Core.DTOS.Estate;
using Aqar.Infrastructure.Extensions;
using Aqar.Data.Model;
using Aqar.Core.Enums;

namespace Aqar.Infrastructure.Repositories.Estate
{
    public interface IEstateRepository
    {
        Task<PaginatedList<GetEstateDto>> GetPaginatedDataAsync(int pageNumber, int pageSize, EstateType? estateType);
        //Task<bool> Create(CreateEstateDTO input);
        Task<Data.Model.Estate> Create(CreateEstateDTO input);
        Task<List<Attachment>> AddAttachment(List<Attachment> attachments);
        Task<Data.Model.Estate> UpdateEstate(UpdateEstateDTO input);
        Task<string> DeleteEstate(int id);
        Task<GetEstateDto> GetEstate(int Id);
    }
}