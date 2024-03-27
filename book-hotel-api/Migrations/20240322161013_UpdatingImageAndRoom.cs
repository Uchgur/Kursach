using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace book_hotel_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingImageAndRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Hotels_HotelId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Image",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_RoomId",
                table: "Image",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Hotels_HotelId",
                table: "Image",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Rooms_RoomId",
                table: "Image",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Hotels_HotelId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Rooms_RoomId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_RoomId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Hotels_HotelId",
                table: "Image",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
