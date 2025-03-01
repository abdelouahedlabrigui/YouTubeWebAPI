using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class PeopleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtlanticHurricanes");

            migrationBuilder.DropTable(
                name: "PacificHurricanes");

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.CreateTable(
                name: "AtlanticHurricanes",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    High_Wind_N = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    High_Wind_NE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    High_Wind_SE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    High_Wind_SW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalId = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Low_Wind_NE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Low_Wind_NW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Low_Wind_SE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Low_Wind_SW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maximum_Wind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minimum_Pressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moderate_Wind_NE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moderate_Wind_NW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moderate_Wind_SE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moderate_Wind_SW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    High_Wind_N = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    High_Wind_NE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    High_Wind_SE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    High_Wind_SW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalId = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Low_Wind_NE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Low_Wind_NW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Low_Wind_SE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Low_Wind_SW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maximum_Wind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minimum_Pressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moderate_Wind_NE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moderate_Wind_NW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moderate_Wind_SE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moderate_Wind_SW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacificHurricanes", x => x.ID);
                });
        }
    }
}
