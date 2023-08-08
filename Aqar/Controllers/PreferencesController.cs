using Aqar.Infrastructure.Repositories.Preferences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aqar.Controllers
{
    [ApiController]
    [Authorize]
    public class PreferencesController : ApiBaseController
    {
        private readonly IPreferencesRepository _preferencesRepository;
        public PreferencesController(IPreferencesRepository preferencesRepository)
        {
            _preferencesRepository = preferencesRepository;
        }

        [HttpGet]
        [Route("api/preferences/getUserFavoriteEstates")]
        public async Task<IActionResult> GetUserFavoriteEstates()
        {
            return Ok(await _preferencesRepository.GetUserFavoriteEstates(LoggedInUser));
        }

        [HttpPost]
        [Route("api/preferences/addToFavorite")]
        public async Task<IActionResult> AddToFavorite(int estateId)
        {
            return Ok(await _preferencesRepository.AddToFavorite(LoggedInUser, estateId));
        }


        [HttpDelete]
        [Route("api/preferences/deleteFromFavorite")]
        public async Task<IActionResult> DeleteFromFavorite(int estateId)
        {
            return Ok(await _preferencesRepository.DeleteFromFavorite(LoggedInUser, estateId));
        }
    }
}
