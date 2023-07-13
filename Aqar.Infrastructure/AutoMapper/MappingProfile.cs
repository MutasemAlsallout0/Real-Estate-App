using Aqar.Core.DTOS.Auth.Request;
using Aqar.Data.Model;
using AutoMapper;

namespace Aqar.Infrastructure.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<RegisterRequest, AppUser>();
             
            #endregion



        }

    }
}