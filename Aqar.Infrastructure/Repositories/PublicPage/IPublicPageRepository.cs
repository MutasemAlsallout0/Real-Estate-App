using Aqar.Data.Model;
using static Aqar.Infrastructure.Repositories.PublicPage.PublicPageRepository;

namespace Aqar.Infrastructure.Repositories.PublicPage
{
    public interface IPublicPageRepository
    {
        Task<PagedResult<AppUser>> SearchOfficesByNameAsync(string officeName, int page, int pageSize);
        Task<Data.Model.PublicPage> CreatePublicPageForOfficeOwner(string userId);
        Task<List<EstateWithOfficeDetails>> GetEstatesWithOfficeDetailsForUser(string userId);
        Task<string> FollowPublicPage(string userId, int publicPageId);
        Task<string> UnFollowPublicPage(string userId);
    }
}