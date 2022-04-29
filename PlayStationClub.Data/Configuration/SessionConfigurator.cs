using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayStationClub.Data.Entity;

namespace PlayStationClub.Data.Configuration
{
    public class SessionConfigurator : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(s => s.DateTime).IsRequired();
            builder.Property(s => s.Duration).IsRequired();
            builder.Property(s => s.PlayerNumber).IsRequired().HasDefaultValue(1);

            builder.HasOne(s => s.Room)
                .WithMany(r => r.Sessions)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(session => session.Services)
                .WithMany(service => service.Sessions)
                .UsingEntity(e => e.HasData(
                    new { SessionsId = 1, ServicesId = 1, },
                    new { SessionsId = 2, ServicesId = 2, },
                    new { SessionsId = 2, ServicesId = 3, },
                    new { SessionsId = 3, ServicesId = 1, },
                    new { SessionsId = 3, ServicesId = 2, },
                    new { SessionsId = 3, ServicesId = 3, }
                    ));
            builder.HasOne(s => s.PlayStationClubUser)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s=>s.PlayStationClubUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.Review)
                .WithOne(r => r.Session)
                .HasForeignKey<Session>(s => s.ReviewId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(
                new Session
                {
                    Id = 1,
                    DateTime = new DateTime(2022, 3, 4),
                    Duration = new TimeSpan(1, 0, 0),
                    PlayerNumber = 2,
                    RoomId = 1,
                    PlayStationClubUserId = "16e6ab58-6b4c-42d5-80eb-d33c0dee0273",
                    ReviewId = 1
                },
                new Session
                {
                    Id = 2,
                    DateTime = new DateTime(2022, 3, 9),
                    Duration = new TimeSpan(2, 0, 0),
                    PlayerNumber = 4,
                    RoomId = 3,
                    PlayStationClubUserId = "16e6ab58-6b4c-42d5-80eb-d33c0dee0273",
                    ReviewId = 2
                },
                new Session
                {
                    Id = 3,
                    DateTime = new DateTime(2022, 3, 20),
                    Duration = new TimeSpan(3, 0, 0),
                    PlayerNumber = 6,
                    RoomId = 4,
                    PlayStationClubUserId = "16e6ab58-6b4c-42d5-80eb-d33c0dee0273"
                });
        }
    }
}
