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
