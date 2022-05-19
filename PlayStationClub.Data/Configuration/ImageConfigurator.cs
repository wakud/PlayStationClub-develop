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
                new Image { Id = 1, FileName = "game-mortal-kombat.svg" },
                new Image { Id = 2, FileName = "photo-fight.png", RoomId = 1 },
                new Image { Id = 4, FileName = "photo-sports.png", RoomId = 2 },
                new Image { Id = 3, FileName = "photo-race.png", RoomId = 3 },
                new Image { Id = 5, FileName = "photo-team.png", RoomId = 4 },
                new Image { Id = 6, FileName = "ciberpunk.jpg" },
                new Image { Id = 7, FileName = "IceAge.png" },
                new Image { Id = 8, FileName = "NinjaWarrior.jpg" },
                new Image { Id = 9, FileName = "Pes2020.jpg" },
                new Image { Id = 10, FileName = "MK-11-Aftermath.jpg", RoomId = 1 },
                new Image { Id = 11, FileName = "MK-11-Aftermath-10.jpg", RoomId = 1 },
                new Image { Id = 12, FileName = "MK-11-Aftermath-12.jpg", RoomId = 1 }
                );
        }
    }
}
