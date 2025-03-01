using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class TransformersClosingTheGapMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LlmNamedEntityRecognitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<int>(type: "int", nullable: false),
                    End = table.Column<int>(type: "int", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlmNamedEntityRecognitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LlmQuestionAnswerings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: false),
                    Start = table.Column<int>(type: "int", nullable: false),
                    End = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlmQuestionAnswerings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LlmTextClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlmTextClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LlmTextGenerations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneratedText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlmTextGenerations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LlmTextSummarizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummaryText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlmTextSummarizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LlmTextTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranslationText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranslationLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlmTextTranslations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LlmNamedEntityRecognitions");

            migrationBuilder.DropTable(
                name: "LlmQuestionAnswerings");

            migrationBuilder.DropTable(
                name: "LlmTextClassifications");

            migrationBuilder.DropTable(
                name: "LlmTextGenerations");

            migrationBuilder.DropTable(
                name: "LlmTextSummarizations");

            migrationBuilder.DropTable(
                name: "LlmTextTranslations");
        }
    }
}
