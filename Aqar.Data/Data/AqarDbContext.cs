using Aqar.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aqar.Data.DataLayer
{
    public class AqarDbContext : IdentityDbContext<AppUser>
    {
        public AqarDbContext(DbContextOptions<AqarDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .Entity<AppUser>()
                   .Property(e => e.UserType)
                               .HasConversion<string>();
            builder.Entity<Following>().HasKey(x => new { x.UserId, x.PublicPageId });
            builder.Entity<Preferences>().HasKey(x => new { x.UserId, x.EstateId });

            #region Address
            builder.Entity<Address>().HasDiscriminator<string>("Discriminator")
                .HasValue<Country>("Country")
                .HasValue<City>("City")
                .HasValue<Street>("Street");
            #endregion

            
        }

 

        #region Address
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }  
        public DbSet<Street> Streets { get; set; }
        #endregion
        public DbSet<PublicPage> PublicPages { get; set; }
        public DbSet<Estate> Estates { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Preferences> Preferences { get; set; }



    }
}