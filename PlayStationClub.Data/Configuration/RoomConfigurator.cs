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
    public class RoomConfigurator : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(r => r.Name).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Description).IsRequired();
            builder.Property(r => r.PlayersNumber).IsRequired().HasDefaultValue(1);
            builder.Property(r => r.Price).IsRequired();

            builder.HasMany(r => r.Images)
                .WithOne(i => i.Room)
                .HasForeignKey(i => i.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Room
                {
                    Id = 1,
                    Name = "fight lobby",
                    Description = "Серьезная игра требует серьезного подхода. UFC, Assassin’s или бессмертный Mortal Kombat – выбирай то, что хочешь, и играй! 50 дюймовый экран, милитари пуфаны, 4 игрока – ничего лишнего, только суть.",
                    PlayersNumber = 4,
                    Price = 90
                },
                new Room
                {
                    Id=2,
                    Name="sport lobby",
                    Description= "Удобные пуфики, атмосфера, друзья – что еще нужно? Хочешь больше? У нас есть напитки и закуски, а также многообразие вкусного табака для кальяна. Прекрасный отдых и качественную игру мы тебе гарантируем. Правило “кратность двум” - цена на комнату зависит от количества человек. Если вы играете двумя джойстиками, но вас, например шестеро, вы будете запущены на 3 джойстика Признайся, что может быть лучше футбола и пива в хорошей компании? Разве что очки виртуальной реальности с реально крутыми играми.",
                    PlayersNumber=10,
                    Price = 165
                },
                new Room
                {
                    Id = 3,
                    Name = "race lobby",
                    Description = "Просторная комната с громки звуком стопроцентно подарят ощущение переполненного стадиона на твоем концерте. Кстати, ты можешь выбрать и любую другую игру, как-никак, хозяин Ferrari сам себе устанавливает правила. А еще мы предложим тебе приличный ассортимент напитков и внушительный перечень кальянов. Что еще круче – ты можешь заказать очки виртуальной реальности с классной подборкой игр. Здесь все для твоего удовольствия!",
                    PlayersNumber = 6,
                    Price = 120
                },
                new Room
                {
                    Id = 4,
                    Name = "team lobby",
                    Description = "Идеальное место для твоей тусовки! Стильный интерьер, комфортные бинбэги, проектор Full HD и объемный звук 5.1 – все это гарантия настоящего удовольствия. Кроме того, у нас есть напитки и закуски на выбор. Вы можете заказать кальян на любой вкус прямо в комнату, что сделает отдых еще приятней. Хочешь больше драйва? Закажи в комнату Guitar Hero. Еще больше? Закажи виртуальную реальность – у нас широкая подборка игр. Тебе точно понравится!",
                    PlayersNumber = 15,
                    Price = 220
                }
                );
        }
    }
}
