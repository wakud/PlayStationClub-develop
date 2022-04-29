using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayStationClub.Data.Configuration;
using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayStationClub.Data
{
    public class PlayStationClubDbContext : IdentityDbContext<PlayStationClubUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public PlayStationClubDbContext(DbContextOptions<PlayStationClubDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ImageConfigurator()); 
            builder.ApplyConfiguration(new CategoryConfigurator());
            builder.ApplyConfiguration(new GameConfigurator());
            builder.ApplyConfiguration(new RoomConfigurator());
            builder.ApplyConfiguration(new ServiceConfigurator());
            builder.ApplyConfiguration(new SessionConfigurator());
            builder.ApplyConfiguration(new ReviewConfigurator());
        }
    }
}
