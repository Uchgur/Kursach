using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace book_hotel_api.Migrations
{
    /// <inheritdoc />
    public partial class AddingCountryToHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Hotels");
        }
    }
}
