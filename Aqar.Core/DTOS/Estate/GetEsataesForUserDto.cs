using Aqar.Core.DTOS.Auth.Response;

namespace Aqar.Core.DTOS.Estate
{
    public class GetEsataesForUserDto
    {
        public List<GetEstateDto> GetEstates { get; set; }
        public GetUserDto GetUserInfo { get; set; }
    }
}