using Aqar.Core.DTOS.ApiBase;
using Aqar.Core.DTOS.Auth.Request;
using Aqar.Core.DTOS.Estate;
using Aqar.Core.DTOS.PublicPage;
using Aqar.Data.Model;
using AutoMapper;

namespace Aqar.Infrastructure.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<CustomerRegisterRequest, AppUser>().ReverseMap();
            CreateMap<UserModel, AppUser>().ReverseMap();

            #endregion

            CreateMap<PublicPageDto, PublicPage>().ReverseMap();



            #region Estate
            CreateMap<EstateDTO, Estate>().ReverseMap()
                .ForMember(x => x.EstateType, x => x.MapFrom(y => y.EstateType.ToString()))
                .ForMember(x => x.ContractType, x => x.MapFrom(y => y.ContractType.ToString()));
            CreateMap<CreateEstateDTO, Estate>().ReverseMap();
            CreateMap<UpdateEstateDTO, Estate>().ReverseMap();
            CreateMap<GetEstateDto, Estate>().ReverseMap();

            #endregion








        }

    }
}