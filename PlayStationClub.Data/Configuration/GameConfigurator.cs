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
    public class GameConfigurator : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
            builder.Property(g => g.Description).IsRequired();
            builder.Property(g => g.PlayersNumber).IsRequired().HasDefaultValue(1);

            builder.HasOne(g => g.Image)
                .WithOne(i => i.Game)
                .HasForeignKey<Game>(g=>g.ImageId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(g => g.Categories)
                .WithMany(c => c.Games)
                .UsingEntity(e => e.HasData(
                    new { CategoriesId = 1, GamesId = 1 }
                    ));

            builder.HasData(
                new Game
                {
                    Id = 1,
                    Name = "mortal kombat 11",
                    Description = "Новая часть культового файтинга Мортал Комбат, с привычной механикой но множеством нововведений. По мимо новых механик вас ждет обновленная графика и новые персонажи. В остальном это старый добрый МК приходи делать фаталити друзьям.",
                    PlayersNumber = 2,
                    ImageId = 1
                }
                );
        }
    }
}
