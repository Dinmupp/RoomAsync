using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logging.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogLevel",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "Context",
                table: "Logs",
                newName: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Logs",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MessageTemplate",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "MessageTemplate",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "Properties",
                table: "Logs",
                newName: "Context");

            migrationBuilder.AddColumn<string>(
                name: "LogLevel",
                table: "Logs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
