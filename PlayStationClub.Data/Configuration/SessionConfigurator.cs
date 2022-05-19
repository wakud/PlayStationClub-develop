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
                .WithMany(service => service.Sessions);
            builder.HasOne(s => s.PlayStationClubUser)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s=>s.PlayStationClubUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.Review)
                .WithOne(r => r.Session)
                .HasForeignKey<Session>(s => s.ReviewId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
