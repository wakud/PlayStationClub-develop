using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayStationClub.Data.Migrations
{
    public partial class SeedRoomData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "Name", "PlayersNumber", "Price" },
                values: new object[,]
                {
                    { 2, "Удобные пуфики, атмосфера, друзья – что еще нужно? Хочешь больше? У нас есть напитки и закуски, а также многообразие вкусного табака для кальяна. Прекрасный отдых и качественную игру мы тебе гарантируем. Правило “кратность двум” - цена на комнату зависит от количества человек. Если вы играете двумя джойстиками, но вас, например шестеро, вы будете запущены на 3 джойстика Признайся, что может быть лучше футбола и пива в хорошей компании? Разве что очки виртуальной реальности с реально крутыми играми.", "sport lobby", (byte)10, 165m },
                    { 3, "Просторная комната с громки звуком стопроцентно подарят ощущение переполненного стадиона на твоем концерте. Кстати, ты можешь выбрать и любую другую игру, как-никак, хозяин Ferrari сам себе устанавливает правила. А еще мы предложим тебе приличный ассортимент напитков и внушительный перечень кальянов. Что еще круче – ты можешь заказать очки виртуальной реальности с классной подборкой игр. Здесь все для твоего удовольствия!", "race lobby", (byte)6, 120m },
                    { 4, "Идеальное место для твоей тусовки! Стильный интерьер, комфортные бинбэги, проектор Full HD и объемный звук 5.1 – все это гарантия настоящего удовольствия. Кроме того, у нас есть напитки и закуски на выбор. Вы можете заказать кальян на любой вкус прямо в комнату, что сделает отдых еще приятней. Хочешь больше драйва? Закажи в комнату Guitar Hero. Еще больше? Закажи виртуальную реальность – у нас широкая подборка игр. Тебе точно понравится!", "team lobby", (byte)15, 220m }
                });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoomId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoomId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoomId",
                value: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoomId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoomId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoomId",
                value: 1);
        }
    }
}
