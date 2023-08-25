using Aqar.Core.DTOS.ApiBase;
using Aqar.Core.DTOS.Auth.Response;
using Aqar.Core.DTOS.Estate;
using Aqar.Data.Model;
using static Aqar.Infrastructure.Repositories.PublicPage.PublicPageRepository;

namespace Aqar.Infrastructure.Repositories.PublicPage
{
    public interface IPublicPageRepository
    {
        Task<PagedResult<GetUserProfileDto>> SearchOfficesByNameAsync(string officeName, int page, int pageSize);
        Task<Data.Model.PublicPage> CreatePublicPageForOfficeOwner(string userId);
        Task<List<GetEstateDto>> GetEstateseDetailsForUserLogin(UserModel currentUser);
        Task<string> FollowPublicPage(UserModel currentUser, int publicPageId);
        Task<string> UnFollowPublicPage(UserModel currentUser);
        Task<GetEsataesForUserDto> GetEstatesForAnyUser(string UserId);
    }
}