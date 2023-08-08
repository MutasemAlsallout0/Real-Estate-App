using Aqar.Core.DTOS.ApiBase;

namespace Aqar.Infrastructure.Repositories.Preferences
{
    public interface IPreferencesRepository
    {
        Task<string> AddToFavorite(UserModel currentUser, int estateId);
        Task<List<Data.Model.Estate>> GetUserFavoriteEstates(UserModel currentUser);
        Task<string> DeleteFromFavorite(UserModel currentUser,  int estateId);
    }
}