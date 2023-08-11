using Aqar.Core.DTOS.ApiBase;
using Aqar.Core.DTOS.Estate;

namespace Aqar.Infrastructure.Repositories.Preferences
{
    public interface IPreferencesRepository
    {
        Task<string> AddToFavorite(UserModel currentUser, int estateId);
        Task<List<GetEstateDto>> GetUserFavoriteEstates(UserModel currentUser);
        Task<string> DeleteFromFavorite(UserModel currentUser,  int estateId);
    }
}