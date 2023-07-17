using Aqar.Data.DataLayer;
using Aqar.Data.Model;
using Aqar.Infrastructure.AutoMapper;
using Aqar.Infrastructure.HelperServices.EmailHelper;
using Aqar.Infrastructure.HelperServices.ImageHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aqar.Infrastructure.DataLayer
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDataLayer(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContext<AqarDbContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection"));
            });

            services.AddDatabaseDeveloperPageExceptionFilter();


            //        services.AddIdentity<AppUser, IdentityRole>()
            //.AddEntityFrameworkStores<AqarDbContext>().AddDefaultUI()
            //.AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 6;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = true;
                config.SignIn.RequireConfirmedAccount = true;
                config.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<AqarDbContext>().AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IEmailService,EmailService>();

            return services;

        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            Assembly assembly = typeof(DependencyInjection).GetTypeInfo().Assembly;

            var allRepositroies = assembly.GetTypes().
                Where(type => type.Name.EndsWith("Repository")//Repository 
              );

            foreach (var repository in allRepositroies)
            {
                if (repository.IsGenericType) return;
                Console.WriteLine($"{repository.FullName} 1");
                var allInterface = repository.GetInterfaces();
                var mainInterFace = allInterface.Except(allInterface.SelectMany(i => i.GetInterfaces()));
                foreach (var iRepository in mainInterFace)
                {
                    if (iRepository.IsGenericType) return;
                    Console.WriteLine($"{iRepository.FullName} 2");
                    services.AddScoped(iRepository, repository);
                }
            }

        }
    }
}