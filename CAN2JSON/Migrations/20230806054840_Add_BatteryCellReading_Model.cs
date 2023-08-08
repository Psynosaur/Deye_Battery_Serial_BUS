using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAN2JSON.Migrations
{
    /// <inheritdoc />
    public partial class Add_BatteryCellReading_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatteryCellReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<int>(type: "INTEGER", nullable: false),
                    SlaveNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Cell01 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell02 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell03 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell04 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell05 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell06 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell07 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell08 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell09 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell10 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell11 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell12 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell13 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell14 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell15 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    Cell16 = table.Column<decimal>(type: "decimal(4, 3)", nullable: false),
                    MinPos = table.Column<decimal>(type: "decimal(0, 0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryCellReadings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatteryCellReadings");
        }
    }
}
