using Aqar.Data.DataLayer;
using Aqar.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Aqar.Infrastructure.Repositories.Preferences
{
    public class PreferencesRepository : IPreferencesRepository
    {
        private readonly AqarDbContext _context;

        public PreferencesRepository(AqarDbContext context)
        {
            _context = context;
        }

        public async Task<List<Data.Model.Estate>> GetUserFavoriteEstates(string userId)
        {
            //validate ids
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new ServiceValidationException("لا يوجد المستخدم");

            return await _context.Preferences.Where(uf => uf.UserId == userId).Select(uf => uf.Estate).ToListAsync();

        }


        public async Task<string> AddToFavorite(string userId, int estateId)
        {
            //validate ids
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new ServiceValidationException("لا يوجد المستخدم");


            var estate = await _context.Estates.FindAsync(estateId);
            if (estate == null) throw new ServiceValidationException("لا يوجد العقار");

            //validate if he already added it to fav
            var userFav = new Data.Model.Preferences { UserId = userId, EstateId = estateId };

            var result = await _context.Preferences.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == userFav.UserId && u.EstateId == userFav.EstateId);

            if (result != null) throw new ServiceValidationException("تم اضافته بالفعل الى المفضلة");

            await _context.Preferences.AddAsync(userFav);
            await _context.SaveChangesAsync();
            return "تمت العملية بنجاح";
                

        }

        public async Task<string> DeleteFromFavorite(string userId, int estateId)
        {
            //validate ids
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new ServiceValidationException("لا يوجد المستخدم");

            var estate = await _context.Estates.FindAsync(estateId);
            if (estate == null) throw new ServiceValidationException("لا يوجد العقار");

            //validate if its exist on database 

            var result = await _context.Preferences.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == userId && u.EstateId == estateId);

            if (result == null) throw new ServiceValidationException("لا يوجد العقار في المفضلة");

            _context.Preferences.Remove(result);
            await _context.SaveChangesAsync();
            return "تمت العملية بنجاح";
        }


    }
}