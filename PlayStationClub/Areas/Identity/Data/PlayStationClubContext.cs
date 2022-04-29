using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayStationClub.Data.Configuration;

namespace PlayStationClub.Data
{
    public class PlayStationClubContext : IdentityDbContext<PlayStationClubUser>
    {
        public PlayStationClubContext(DbContextOptions<PlayStationClubContext> options)
            : base(options)
        {
            //TODO: це зробив для створення бд без міграції (можна видалити)
            //Database.EnsureDeleted();     //видалення бд
            //Database.EnsureCreated();     //створення БД
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
