using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class TechnicalIndicatorsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnicalIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Close = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    Open = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    SMA_20 = table.Column<double>(type: "float", nullable: false),
                    EMA_20 = table.Column<double>(type: "float", nullable: false),
                    RSI_14 = table.Column<double>(type: "float", nullable: false),
                    MACD = table.Column<double>(type: "float", nullable: false),
                    MACD_Signal = table.Column<double>(type: "float", nullable: false),
                    Upper_BB = table.Column<double>(type: "float", nullable: false),
                    Lower_BB = table.Column<double>(type: "float", nullable: false),
                    Stochastic_K = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalIndicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalIndicatorsVisualizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Visualization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalIndicatorsVisualizations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicalIndicators");

            migrationBuilder.DropTable(
                name: "TechnicalIndicatorsVisualizations");
        }
    }
}
