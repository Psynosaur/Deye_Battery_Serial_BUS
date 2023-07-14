using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAN2JSON.Migrations
{
    /// <inheritdoc />
    public partial class DataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Watts",
                table: "BmsReadings",
                type: "decimal(5, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Voltage",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "StateOfHealth",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "StateOfCharge",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "FullChargedRestingVoltage",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargeLimit",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentLimit",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChargeVoltage",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChargeCurrentLimitMax",
                table: "BmsReadings",
                type: "decimal(3, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChargeCurrentLimit",
                table: "BmsReadings",
                type: "decimal(3, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageLow",
                table: "BmsReadings",
                type: "decimal(4, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageHigh",
                table: "BmsReadings",
                type: "decimal(4, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageDelta",
                table: "BmsReadings",
                type: "decimal(4, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "BmsTemperatureLow",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "BmsTemperatureHigh",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryCutoffVoltage",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryCapacity",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amps",
                table: "BmsReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureTwo",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureOne",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureMos",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "StateOfHealth",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "StateOfCharge",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedTotal",
                table: "BatteryReadings",
                type: "decimal(7, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cycles",
                table: "BatteryReadings",
                type: "decimal(4, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentLimitMax",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentLimit",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChargedTotal",
                table: "BatteryReadings",
                type: "decimal(7, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageLow",
                table: "BatteryReadings",
                type: "decimal(4, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageHigh",
                table: "BatteryReadings",
                type: "decimal(4, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageDelta",
                table: "BatteryReadings",
                type: "decimal(4, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryVoltage",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryCurrent",
                table: "BatteryReadings",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Watts",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Voltage",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StateOfHealth",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StateOfCharge",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FullChargedRestingVoltage",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargeLimit",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentLimit",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChargeVoltage",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChargeCurrentLimitMax",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChargeCurrentLimit",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageLow",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageHigh",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageDelta",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BmsTemperatureLow",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BmsTemperatureHigh",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryCutoffVoltage",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryCapacity",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amps",
                table: "BmsReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureTwo",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureOne",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureMos",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StateOfHealth",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StateOfCharge",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedTotal",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cycles",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentLimitMax",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentLimit",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChargedTotal",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageLow",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageHigh",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CellVoltageDelta",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryVoltage",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryCurrent",
                table: "BatteryReadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");
        }
    }
}
