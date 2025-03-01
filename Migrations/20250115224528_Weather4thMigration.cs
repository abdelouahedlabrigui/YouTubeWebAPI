using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Weather4thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Humidities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherVisualizationId = table.Column<int>(type: "int", nullable: false),
                    Mean = table.Column<double>(type: "float", nullable: false),
                    Median = table.Column<double>(type: "float", nullable: false),
                    Std_dev = table.Column<double>(type: "float", nullable: false),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humidities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temperatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherVisualizationId = table.Column<int>(type: "int", nullable: false),
                    Mean = table.Column<double>(type: "float", nullable: false),
                    Median = table.Column<double>(type: "float", nullable: false),
                    Std_dev = table.Column<double>(type: "float", nullable: false),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WindSpeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherVisualizationId = table.Column<int>(type: "int", nullable: false),
                    Mean = table.Column<double>(type: "float", nullable: false),
                    Median = table.Column<double>(type: "float", nullable: false),
                    Std_dev = table.Column<double>(type: "float", nullable: false),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindSpeeds", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Humidities");

            migrationBuilder.DropTable(
                name: "Temperatures");

            migrationBuilder.DropTable(
                name: "WindSpeeds");
        }
    }
}
