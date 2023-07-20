using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAN2JSON.Migrations
{
    /// <inheritdoc />
    public partial class Int_Date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Date",
                table: "BmsReadings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Date",
                table: "BatteryReadings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "BmsReadings");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BatteryReadings");
        }
    }
}
