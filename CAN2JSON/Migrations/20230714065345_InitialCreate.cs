using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAN2JSON.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BmsReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChargeCurrentLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    ChargeCurrentLimitMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    Voltage = table.Column<decimal>(type: "TEXT", nullable: false),
                    Amps = table.Column<decimal>(type: "TEXT", nullable: false),
                    Watts = table.Column<decimal>(type: "TEXT", nullable: false),
                    Temperature = table.Column<decimal>(type: "TEXT", nullable: false),
                    StateOfCharge = table.Column<decimal>(type: "TEXT", nullable: false),
                    StateOfHealth = table.Column<decimal>(type: "TEXT", nullable: false),
                    CellVoltageHigh = table.Column<decimal>(type: "TEXT", nullable: false),
                    CellVoltageLow = table.Column<decimal>(type: "TEXT", nullable: false),
                    CellVoltageDelta = table.Column<decimal>(type: "TEXT", nullable: false),
                    BmsTemperatureHigh = table.Column<decimal>(type: "TEXT", nullable: false),
                    BmsTemperatureLow = table.Column<decimal>(type: "TEXT", nullable: false),
                    BatteryCapacity = table.Column<decimal>(type: "TEXT", nullable: false),
                    ChargeVoltage = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    DischargeLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    BatteryCutoffVoltage = table.Column<decimal>(type: "TEXT", nullable: false),
                    FullChargedRestingVoltage = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BmsReadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatteryReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SlaveNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BatteryVoltage = table.Column<decimal>(type: "TEXT", nullable: false),
                    BatteryCurrent = table.Column<decimal>(type: "TEXT", nullable: false),
                    StateOfCharge = table.Column<decimal>(type: "TEXT", nullable: false),
                    StateOfHealth = table.Column<decimal>(type: "TEXT", nullable: false),
                    CellVoltageHigh = table.Column<decimal>(type: "TEXT", nullable: false),
                    CellVoltageLow = table.Column<decimal>(type: "TEXT", nullable: false),
                    CellVoltageDelta = table.Column<decimal>(type: "TEXT", nullable: false),
                    TemperatureOne = table.Column<decimal>(type: "TEXT", nullable: false),
                    TemperatureTwo = table.Column<decimal>(type: "TEXT", nullable: false),
                    TemperatureMos = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentLimitMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    ChargedTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    DischargedTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Cycles = table.Column<decimal>(type: "TEXT", nullable: false),
                    BmsReadingId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatteryReadings_BmsReadings_BmsReadingId",
                        column: x => x.BmsReadingId,
                        principalTable: "BmsReadings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatteryReadings_BmsReadingId",
                table: "BatteryReadings",
                column: "BmsReadingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatteryReadings");

            migrationBuilder.DropTable(
                name: "BmsReadings");
        }
    }
}
