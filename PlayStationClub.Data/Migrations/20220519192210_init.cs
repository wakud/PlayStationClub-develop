using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayStationClub.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceSession",
                keyColumns: new[] { "ServicesId", "SessionsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ServiceSession",
                keyColumns: new[] { "ServicesId", "SessionsId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ServiceSession",
                keyColumns: new[] { "ServicesId", "SessionsId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ServiceSession",
                keyColumns: new[] { "ServicesId", "SessionsId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ServiceSession",
                keyColumns: new[] { "ServicesId", "SessionsId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "ServiceSession",
                keyColumns: new[] { "ServicesId", "SessionsId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "FileName",
                value: "game-mortal-kombat.svg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "FileName",
                value: "photo-fight.png");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "FileName",
                value: "photo-race.png");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                column: "FileName",
                value: "photo-sports.png");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                column: "FileName",
                value: "photo-team.png");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "FileName", "RoomId" },
                values: new object[,]
                {
                    { 6, "ciberpunk.jpg", null },
                    { 7, "IceAge.png", null },
                    { 8, "NinjaWarrior.jpg", null },
                    { 9, "Pes2020.jpg", null },
                    { 10, "MK-11-Aftermath.jpg", 1 },
                    { 11, "MK-11-Aftermath-10.jpg", 1 },
                    { 12, "MK-11-Aftermath-12.jpg", 1 }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "ImageId", "Name", "PlayersNumber" },
                values: new object[,]
                {
                    { 2, "Ну тут опис гри CyberPunk бла-бла-бла ...", 6, "CyberPunk", (byte)10 },
                    { 5, "Ну тут опис гри IceAge бла-бла-бла ...", 7, "IceAge", (byte)2 },
                    { 4, "Ну тут опис гри NinjaWarrior бла-бла-бла ...", 8, "NinjaWarrior", (byte)4 },
                    { 3, "Ну тут опис гри FIFA2020 бла-бла-бла ...", 9, "FIFA2020", (byte)2 }
                });

            migrationBuilder.InsertData(
                table: "CategoryGame",
                columns: new[] { "CategoriesId", "GamesId" },
                values: new object[,]
                {
                    { 3, 2 },
                    { 3, 5 },
                    { 2, 4 },
                    { 2, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryGame",
                keyColumns: new[] { "CategoriesId", "GamesId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CategoryGame",
                keyColumns: new[] { "CategoriesId", "GamesId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "CategoryGame",
                keyColumns: new[] { "CategoriesId", "GamesId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "CategoryGame",
                keyColumns: new[] { "CategoriesId", "GamesId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "FileName",
                value: "game-mortal-kombat");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "FileName",
                value: "photo-fight");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "FileName",
                value: "photo-race");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                column: "FileName",
                value: "photo-sports");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                column: "FileName",
                value: "photo-team");

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "d1", "s1", 10m },
                    { 2, "d2", "s2", 20m },
                    { 3, "d3", "s3", 30m }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "DateTime", "Duration", "PlayStationClubUserId", "PlayerNumber", "ReviewId", "RoomId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), "16e6ab58-6b4c-42d5-80eb-d33c0dee0273", (byte)2, 1, 1 },
                    { 2, new DateTime(2022, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0), "16e6ab58-6b4c-42d5-80eb-d33c0dee0273", (byte)4, 2, 3 },
                    { 3, new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0), "16e6ab58-6b4c-42d5-80eb-d33c0dee0273", (byte)6, null, 4 }
                });

            migrationBuilder.InsertData(
                table: "ServiceSession",
                columns: new[] { "ServicesId", "SessionsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 }
                });
        }
    }
}
