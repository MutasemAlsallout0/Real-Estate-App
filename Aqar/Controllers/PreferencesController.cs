using Aqar.Infrastructure.Repositories.Preferences;
using Microsoft.AspNetCore.Mvc;

namespace Aqar.Controllers
{
    [ApiController]
    public class PreferencesController : ControllerBase
    {
        private readonly IPreferencesRepository _preferencesRepository;
        public PreferencesController(IPreferencesRepository preferencesRepository)
        {
            _preferencesRepository = preferencesRepository;
        }

        [HttpGet]
        [Route("api/preferences/getUserFavoriteEstates/{userId}")]
        public async Task<IActionResult> GetUserFavoriteEstates(string userId)
        {
            return Ok(await _preferencesRepository.GetUserFavoriteEstates(userId));
        }

        [HttpPost]
        [Route("api/preferences/addToFavorite")]
        public async Task<IActionResult> AddToFavorite(string userId, int estateId)
        {
            return Ok(await _preferencesRepository.AddToFavorite(userId, estateId));
        }


        [HttpDelete]
        [Route("api/preferences/deleteFromFavorite")]
        public async Task<IActionResult> DeleteFromFavorite(string userId, int estateId)
        {
            return Ok(await _preferencesRepository.DeleteFromFavorite(userId, estateId));
        }
    }
}
