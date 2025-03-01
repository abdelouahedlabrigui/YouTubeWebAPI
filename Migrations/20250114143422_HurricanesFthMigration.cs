using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class HurricanesFthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtlanticHurricanes",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maximum_Wind = table.Column<int>(type: "int", nullable: false),
                    Minimum_Pressure = table.Column<int>(type: "int", nullable: false),
                    Low_Wind_NE = table.Column<int>(type: "int", nullable: false),
                    Low_Wind_SE = table.Column<int>(type: "int", nullable: false),
                    Low_Wind_SW = table.Column<int>(type: "int", nullable: false),
                    Low_Wind_NW = table.Column<int>(type: "int", nullable: false),
                    Moderate_Wind_NE = table.Column<int>(type: "int", nullable: false),
                    Moderate_Wind_SE = table.Column<int>(type: "int", nullable: false),
                    Moderate_Wind_SW = table.Column<int>(type: "int", nullable: false),
                    Moderate_Wind_NW = table.Column<int>(type: "int", nullable: false),
                    High_Wind_NE = table.Column<int>(type: "int", nullable: false),
                    High_Wind_SE = table.Column<int>(type: "int", nullable: false),
                    High_Wind_SW = table.Column<int>(type: "int", nullable: false),
                    High_Wind_N = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtlanticHurricanes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PacificHurricanes",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maximum_Wind = table.Column<int>(type: "int", nullable: false),
                    Minimum_Pressure = table.Column<int>(type: "int", nullable: false),
                    Low_Wind_NE = table.Column<int>(type: "int", nullable: false),
                    Low_Wind_SE = table.Column<int>(type: "int", nullable: false),
                    Low_Wind_SW = table.Column<int>(type: "int", nullable: false),
                    Low_Wind_NW = table.Column<int>(type: "int", nullable: false),
                    Moderate_Wind_NE = table.Column<int>(type: "int", nullable: false),
                    Moderate_Wind_SE = table.Column<int>(type: "int", nullable: false),
                    Moderate_Wind_SW = table.Column<int>(type: "int", nullable: false),
                    Moderate_Wind_NW = table.Column<int>(type: "int", nullable: false),
                    High_Wind_NE = table.Column<int>(type: "int", nullable: false),
                    High_Wind_SE = table.Column<int>(type: "int", nullable: false),
                    High_Wind_SW = table.Column<int>(type: "int", nullable: false),
                    High_Wind_N = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacificHurricanes", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtlanticHurricanes");

            migrationBuilder.DropTable(
                name: "PacificHurricanes");
        }
    }
}
