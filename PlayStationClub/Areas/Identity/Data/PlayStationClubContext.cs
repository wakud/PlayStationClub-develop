using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayStationClub.Data.Configuration;
using PlayStationClub.Infrastructure.ViewModels;
using PlayStationClub.Data.Entity;

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

        public DbSet<PlayStationClub.Infrastructure.ViewModels.GameViewModel> GameViewModel { get; set; }

        public DbSet<PlayStationClub.Data.Entity.Game> Game { get; set; }
    }
}
