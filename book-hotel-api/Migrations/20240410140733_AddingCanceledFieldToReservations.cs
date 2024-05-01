using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace book_hotel_api.Migrations
{
    /// <inheritdoc />
    public partial class AddingCanceledFieldToReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Canceled",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Canceled",
                table: "Reservations");
        }
    }
}
