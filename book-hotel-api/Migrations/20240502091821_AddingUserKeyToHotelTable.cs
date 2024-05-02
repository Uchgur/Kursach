using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace book_hotel_api.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserKeyToHotelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Hotels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_UserId",
                table: "Hotels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_UserId",
                table: "Hotels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_UserId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_UserId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hotels");
        }
    }
}
