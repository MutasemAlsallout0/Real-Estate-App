using Aqar.Core.DTOS.ApiBase;
using Aqar.Data.Model;
using static Aqar.Infrastructure.Repositories.PublicPage.PublicPageRepository;

namespace Aqar.Infrastructure.Repositories.PublicPage
{
    public interface IPublicPageRepository
    {
        Task<PagedResult<AppUser>> SearchOfficesByNameAsync(string officeName, int page, int pageSize);
        Task<Data.Model.PublicPage> CreatePublicPageForOfficeOwner(string userId);
        Task<List<EstateWithOfficeDetails>> GetEstatesWithOfficeDetailsForUser(UserModel currentUser);
        Task<string> FollowPublicPage(UserModel currentUser, int publicPageId);
        Task<string> UnFollowPublicPage(UserModel currentUser);
    }
}