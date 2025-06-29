using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountryCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "ReservationHolder",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "ReservationHolder");
        }
    }
}
