using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeldCheckInAndSelfCheckOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CheckedInAt",
                table: "Reservations",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CheckedOutAt",
                table: "Reservations",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CheckedInAt",
                table: "ReservationHolder",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CheckedOutAt",
                table: "ReservationHolder",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedInAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CheckedOutAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CheckedInAt",
                table: "ReservationHolder");

            migrationBuilder.DropColumn(
                name: "CheckedOutAt",
                table: "ReservationHolder");
        }
    }
}
