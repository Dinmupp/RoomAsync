using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedReservationHolderDataEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestEmail",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "GuestName",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "GuestPhone",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "ReservationHolderId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReservationHolder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationHolderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHolder", x => x.Id);
                    table.UniqueConstraint("AK_ReservationHolder_ReservationHolderId", x => x.ReservationHolderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationHolderId",
                table: "Reservations",
                column: "ReservationHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationHolder_ReservationHolderId",
                table: "Reservations",
                column: "ReservationHolderId",
                principalTable: "ReservationHolder",
                principalColumn: "ReservationHolderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationHolder_ReservationHolderId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationHolder");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationHolderId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationHolderId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "GuestEmail",
                table: "Reservations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestName",
                table: "Reservations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestPhone",
                table: "Reservations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
