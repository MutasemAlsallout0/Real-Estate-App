using Aqar.Core.DTOS.ApiBase;
using Aqar.Core.DTOS.Estate;
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

        public async Task<List<GetEstateDto>> GetUserFavoriteEstates(UserModel currentUser)
        {
            //validate ids
            var user = await _context.Users.FindAsync(currentUser.Id);
            if (user == null) throw new ServiceValidationException("لا يوجد المستخدم");

            var getEstates =  await _context.Preferences.Where(uf => uf.UserId == currentUser.Id).Select(uf => uf.Estate).Select(x => new GetEstateDto
            {
                Id = x.Id,
                OwnerEstate = x.User.GetFullName(),
                UserImage = x.User.UserImage,
                EstateType = x.DisplayEstateType,
                ContractType = x.DisplayContractType,
                Price = x.Price,
                Area = x.Area,
                Description = x.Description,
                street = x.Street.Name,
                city = x.Street.City.Name,
                country = x.Street.City.Country.Name,
                MainImage = x.MainImage,
                CreateAt = x.CreateAt,

            }).ToListAsync();
            return getEstates;
                                      
        }


        public async Task<string> AddToFavorite(UserModel currentUser ,int estateId)
        {
 


            var estate = await _context.Estates.FindAsync(estateId);
            if (estate == null) throw new ServiceValidationException("لا يوجد العقار");

            //validate if he already added it to fav
            var userFav = new Data.Model.Preferences { UserId = currentUser.Id, EstateId = estateId };

            var result = await _context.Preferences.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == userFav.UserId && u.EstateId == userFav.EstateId);

            if (result != null) throw new ServiceValidationException("تم اضافته بالفعل الى المفضلة");

            await _context.Preferences.AddAsync(userFav);
            await _context.SaveChangesAsync();
            return "تمت العملية بنجاح";
                

        }

        public async Task<string> DeleteFromFavorite(UserModel currentUser, int estateId)
        {
 

            var estate = await _context.Estates.FindAsync(estateId);
            if (estate == null) throw new ServiceValidationException("لا يوجد العقار");

            //validate if its exist on database 

            var result = await _context.Preferences.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == currentUser.Id && u.EstateId == estateId);

            if (result == null) throw new ServiceValidationException("لا يوجد العقار في المفضلة");

            _context.Preferences.Remove(result);
            await _context.SaveChangesAsync();
            return "تمت العملية بنجاح";
        }


    }
}