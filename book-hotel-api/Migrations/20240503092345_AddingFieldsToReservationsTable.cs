using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace book_hotel_api.Migrations
{
    /// <inheritdoc />
    public partial class AddingFieldsToReservationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PayOffline",
                table: "Reservations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PayOnline",
                table: "Reservations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PayOffline",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PayOnline",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Reservations");
        }
    }
}
