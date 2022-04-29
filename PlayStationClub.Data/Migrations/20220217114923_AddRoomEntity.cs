using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PlayStationClub.Data.Migrations
{
    public partial class AddRoomEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Images_ImageId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Images",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PlayersNumber = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)1),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "Name", "PlayersNumber", "Price" },
                values: new object[] { 1, "Серьезная игра требует серьезного подхода. UFC, Assassin’s или бессмертный Mortal Kombat – выбирай то, что хочешь, и играй! 50 дюймовый экран, милитари пуфаны, 4 игрока – ничего лишнего, только суть.", "fight lobby", (byte)4, 90m });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "FileName", "RoomId" },
                values: new object[,]
                {
                    { 2, "photo-fight", 1 },
                    { 3, "photo-race", 1 },
                    { 4, "photo-sports", 1 },
                    { 5, "photo-team", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_RoomId",
                table: "Images",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Images_ImageId",
                table: "Games",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Rooms_RoomId",
                table: "Images",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Images_ImageId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Rooms_RoomId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Images_RoomId",
                table: "Images");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Images_ImageId",
                table: "Games",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
