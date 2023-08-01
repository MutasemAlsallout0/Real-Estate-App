namespace Aqar.Infrastructure.Repositories.Preferences
{
    public interface IPreferencesRepository
    {
        Task<string> AddToFavorite(string userId, int estateId);
        Task<List<Data.Model.Estate>> GetUserFavoriteEstates(string userId);
        Task<string> DeleteFromFavorite(string userId, int estateId);
    }
}