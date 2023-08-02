using static Aqar.Infrastructure.Repositories.PublicPage.PublicPageRepository;

namespace Aqar.Infrastructure.Repositories.PublicPage
{
    public interface IPublicPageRepository
    {
        Task<Data.Model.PublicPage> CreatePublicPageForOfficeOwner(string userId, string officeName);
        // Task<List<Data.Model.Estate>> GetEstatesForUser(string userId);
        Task<List<EstateWithOfficeDetails>> GetEstatesWithOfficeDetailsForUser(string userId);
    }
}