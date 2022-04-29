using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayStationClub.Data.Entity;

namespace PlayStationClub.Data.Configuration
{
    public class ImageConfigurator : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(i => i.FileName).IsRequired();
            builder.HasIndex(i => i.FileName).IsUnique();


            builder.HasData(
                new Image { Id = 1, FileName = "game-mortal-kombat.svg"},
                new Image { Id = 2, FileName = "photo-fight.png", RoomId=1 },
                new Image { Id = 4, FileName = "photo-sports.png", RoomId = 2 },
                new Image { Id = 3, FileName = "photo-race.png", RoomId = 3 },
                new Image { Id = 5, FileName = "photo-team.png", RoomId = 4 }
                );
        }
    }
}
