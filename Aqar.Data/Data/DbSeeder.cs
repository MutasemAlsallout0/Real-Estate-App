using Aqar.Core.Enums;
using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

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
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return webHost;
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
                UserImage="defult",
                CreateAt=DateTime.Now,
                UserType = UserType.Admin,
                EmailConfirmed = true,

            };
            await userManger.CreateAsync(user, "Admin@2023");
            await userManger.AddToRoleAsync(user, "Admin");

        }
    }
}