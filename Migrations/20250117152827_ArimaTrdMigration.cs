using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ArimaTrdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArimaVisualizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisualizedTrendsAndSeasonality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisualizedDifferencedTimeSeries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisualizedARIMAModelEvaluation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisualizedForecastFutureValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisualizedARIMAModelSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArimaVisualizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SockPriceAdfTestCriticalValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stationarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriticalValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SockPriceAdfTestCriticalValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SockPriceAdfTestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stationarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADFStatistic = table.Column<double>(type: "float", nullable: false),
                    PValue = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SockPriceAdfTestResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SockPriceArimaCoefficients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coefficients = table.Column<double>(type: "float", nullable: false),
                    StandardErrors = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SockPriceArimaCoefficients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SockPriceArimaMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AIC = table.Column<double>(type: "float", nullable: false),
                    BIC = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SockPriceArimaMetrics", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArimaVisualizations");

            migrationBuilder.DropTable(
                name: "SockPriceAdfTestCriticalValues");

            migrationBuilder.DropTable(
                name: "SockPriceAdfTestResults");

            migrationBuilder.DropTable(
                name: "SockPriceArimaCoefficients");

            migrationBuilder.DropTable(
                name: "SockPriceArimaMetrics");
        }
    }
}
