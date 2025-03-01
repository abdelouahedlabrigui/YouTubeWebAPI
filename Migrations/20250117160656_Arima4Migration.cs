using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Arima4Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SockPriceAdfTestCriticalValues");

            migrationBuilder.DropTable(
                name: "SockPriceAdfTestResults");

            migrationBuilder.DropTable(
                name: "SockPriceArimaCoefficients");

            migrationBuilder.DropTable(
                name: "SockPriceArimaMetrics");

            migrationBuilder.CreateTable(
                name: "StockPriceAdfTestCriticalValues",
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
                    table.PrimaryKey("PK_StockPriceAdfTestCriticalValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockPriceAdfTestResults",
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
                    table.PrimaryKey("PK_StockPriceAdfTestResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockPriceArimaCoefficients",
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
                    table.PrimaryKey("PK_StockPriceArimaCoefficients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockPriceArimaMetrics",
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
                    table.PrimaryKey("PK_StockPriceArimaMetrics", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockPriceAdfTestCriticalValues");

            migrationBuilder.DropTable(
                name: "StockPriceAdfTestResults");

            migrationBuilder.DropTable(
                name: "StockPriceArimaCoefficients");

            migrationBuilder.DropTable(
                name: "StockPriceArimaMetrics");

            migrationBuilder.CreateTable(
                name: "SockPriceAdfTestCriticalValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriticalValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stationarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    ADFStatistic = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PValue = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stationarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Coefficients = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardErrors = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    AIC = table.Column<double>(type: "float", nullable: false),
                    BIC = table.Column<double>(type: "float", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SockPriceArimaMetrics", x => x.Id);
                });
        }
    }
}
