using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Aqar.Data.Data
{
    public static class DbSeeder
    {


        public static async Task<IHost> Initialize(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AqarDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await roleManager.InitRoles();
                if (!await userManager.Users.AnyAsync())
                {
                    await userManager.SeedAdmin();
                }
                //await context.InitCountry();
                //await ntcoext.InitCity();
                //await context.InitStreet();

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return webHost;
        }

        public static async Task InitCountry(this AqarDbContext context)
        {

            var countries = new List<Country>
            {
                new Country { Name = "فلسطين" },
             };
            foreach (var country in countries)
            {
                await context.Countries.AddAsync(country);
            }
        }
        public static async Task InitCity(this AqarDbContext context)
        {

            var cities = new List<City>
            {
                new City { Name = "غزة " , CountryId = 43},
                new City { Name = "خان يونس" , CountryId = 43},
                new City { Name = "رفح" , CountryId = 43},
                new City { Name = "بيت لاهيا" , CountryId = 43},
                new City { Name = "جباليا" , CountryId = 43},
                new City { Name = "النصيرات" , CountryId = 43},
                new City { Name = "البريج" , CountryId = 43},
                new City { Name = "المغازي" , CountryId = 43},
                new City { Name = "الزوايدة" , CountryId = 43},
                new City { Name = "دير البلح" , CountryId = 43},

            };
            foreach (var city in cities)
            {
                await context.Cities.AddAsync(city);
            }
        }
        public static async Task InitStreet(this AqarDbContext context)
        {

            var streets = new List<Street>
            {
                new Street { Name = "شارع عمر المختار" , CityId = 54},
                new Street { Name = "شارع الثورة"      , CityId = 54},
                new Street { Name = "شارع النفق"       , CityId = 54},
                new Street { Name = "شارع صلاح الدين"   , CityId = 54},
                new Street { Name = "شارع الوحدة"      , CityId = 54},
                new Street { Name = "شارع الرشيد "     , CityId = 54},
                new Street { Name = "شارع مصطفى حافظ"  , CityId = 54},
                new Street { Name = "شارع حي الدرج"    , CityId = 54},

               new Street { Name = "شارع الترنس"         , CityId = 55},
               new Street { Name = "شارع القدرة"          , CityId = 55},
               new Street { Name = "شارع المدرسة"         , CityId = 55},
               new Street { Name = "شارع الهلال القديم"    , CityId = 55},
               new Street { Name = "شارع القسام"          , CityId = 55},
               new Street { Name = "شارع جمال عبد الناصر" , CityId = 55},
               new Street { Name = "شارع البحر"           , CityId = 55},
               new Street { Name = "شارع السيقلي"         , CityId = 55},

               new Street { Name = "شارع ابو يوسف النجار" , CityId = 56},
               new Street { Name = "شارع عمر بن الخطاب"   , CityId = 56},
               new Street { Name = "شارع الزهراء"          , CityId = 56},
               new Street { Name = "شارع البرازيل"         , CityId = 56},
               new Street { Name = "شارع طه حسين"          , CityId = 56},
               new Street { Name = "شارع المنتزة"          , CityId = 56},

               //new Street { Name = "شارع الشهداء" , CityId = 44},
               //new Street { Name = "شارع صلاح خلف" , CityId = 44},
               //new Street { Name = "شارع الخلفاء الراشدين" , CityId = 44},
               //new Street { Name = "شارع السلام" , CityId = 44},

               //new Street { Name = "شارع الحرية" , CityId = 39},
               //new Street { Name = "شارع حيفا" , CityId = 39},
               //new Street { Name = "شارع القدس" , CityId = 39},
               //new Street { Name = "شارع النزهة" , CityId = 39},
               //new Street { Name = "شارع المحول" , CityId = 39},

               //new Street { Name = "شارع المخيم" , CityId = 40},
               //new Street { Name = "شارع عمر بنت عبد العزيز" , CityId = 40},
               //new Street { Name = "شارع الامل" , CityId = 40},
               //new Street { Name = "شارع فلسطين" , CityId = 40},
               //new Street { Name = "شارع عمر بن الخطاب" , CityId = 40},

               //new Street { Name = "شارع الاندلس" , CityId = 41},
               //new Street { Name = "شارع الشهيد يحيى جابر" , CityId = 41},
               //new Street { Name = "شارع البرج" , CityId = 41},

               //new Street { Name = "شارع خالد بن الوليد" , CityId = 43},
               //new Street { Name = "شارع الخلفاء الراشدين" , CityId = 43},
               //new Street { Name = "شارع النخيل" , CityId = 43},
               //new Street { Name = "شارع العبسة" , CityId = 43},

               //new Street { Name = "شارع خالد بن الوليد" , CityId = 42},
               //new Street { Name = "شارع مغازي الشهداء" , CityId = 42},

               //new Street { Name = "شارع المنشية" , CityId = 38},
               //new Street { Name = "شارع الرملة" , CityId = 38},
               //new Street { Name = "شارع الفلوجة" , CityId = 38},
               //new Street { Name = "شارع الجمعية" , CityId = 38},
               //new Street { Name = "الشارع العام بيت لاهيا" , CityId = 38},

            };
            foreach (var street in streets)
            {
                await context.Streets.AddAsync(street);
            }
        }
        public static async Task InitRoles(this RoleManager<IdentityRole> roleManager)
        {

            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name = UserType.Admin.ToString()},
                new IdentityRole{Name=UserType.Customer.ToString()},
                new IdentityRole{Name=UserType.OfficeOwner.ToString()}

            };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        public static async Task SeedAdmin(this UserManager<AppUser> userManger)
        {

            var user = new AppUser()
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                IsActive = false,
                UserImage = "defult",
                CreateAt = DateTime.Now,
                UserType = UserType.Admin,
                EmailConfirmed = true,

            };
            await userManger.CreateAsync(user, "Admin@2023");
            await userManger.AddToRoleAsync(user, "Admin");

        }
    }
}