﻿using Aqar.Core.DTOS.Estate;
using Aqar.Infrastructure.Extensions;
using Aqar.Data.Model;
using Aqar.Core.Enums;
using Aqar.Core.DTOS.ApiBase;

namespace Aqar.Infrastructure.Repositories.Estate
{
    public interface IEstateRepository
    {
        Task<PaginatedList<GetEstateDto>> GetPaginatedDataAsync(int pageNumber, int pageSize, EstateType? estateType);
         Task<Data.Model.Estate> Create(UserModel currentUser, CreateEstateDTO input);
        Task<List<Attachment>> AddAttachment(List<Attachment> attachments);
        Task<Data.Model.Estate> UpdateEstate(UpdateEstateDTO input);
        Task<string> DeleteEstate(int id);
        Task<GetEstateDto> GetEstate(UserModel currentUser, int Id);
        Task<List<Country>> GetCountries();
        Task<List<City>> GetCities(int countryId);

    }
}