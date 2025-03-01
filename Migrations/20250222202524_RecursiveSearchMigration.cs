using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RecursiveSearchMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Buildings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Companies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Countries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtractEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureExtractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sentiment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentimentScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractedAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractedScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Events = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureExtractions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchBuildingContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Buildings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedChunks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchBuildingContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchCompanyContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Companies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedChunks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchCompanyContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchCountryContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Countries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedChunks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchCountryContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchNationalityContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nationalities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedChunks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchNationalityContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchNonGpeContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NonGpe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedChunks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchNonGpeContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchPersonContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Person = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedChunks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPersonContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NationalityContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nationalities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalityContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonGpeContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NonGpe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonGpeContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Person = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WikiSearchs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiSearchs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingContexts");

            migrationBuilder.DropTable(
                name: "CompanyContexts");

            migrationBuilder.DropTable(
                name: "CountryContexts");

            migrationBuilder.DropTable(
                name: "ExtractEntities");

            migrationBuilder.DropTable(
                name: "FeatureExtractions");

            migrationBuilder.DropTable(
                name: "MatchBuildingContexts");

            migrationBuilder.DropTable(
                name: "MatchCompanyContexts");

            migrationBuilder.DropTable(
                name: "MatchCountryContexts");

            migrationBuilder.DropTable(
                name: "MatchNationalityContexts");

            migrationBuilder.DropTable(
                name: "MatchNonGpeContexts");

            migrationBuilder.DropTable(
                name: "MatchPersonContexts");

            migrationBuilder.DropTable(
                name: "NationalityContexts");

            migrationBuilder.DropTable(
                name: "NonGpeContexts");

            migrationBuilder.DropTable(
                name: "PersonContexts");

            migrationBuilder.DropTable(
                name: "WikiSearchs");
        }
    }
}
