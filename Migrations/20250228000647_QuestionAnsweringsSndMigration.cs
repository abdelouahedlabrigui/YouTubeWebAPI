using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class QuestionAnsweringsSndMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Response",
                table: "QuestionAnswerings",
                newName: "Start");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "QuestionAnswerings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "End",
                table: "QuestionAnswerings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Score",
                table: "QuestionAnswerings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "QuestionAnswerings");

            migrationBuilder.DropColumn(
                name: "End",
                table: "QuestionAnswerings");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "QuestionAnswerings");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "QuestionAnswerings",
                newName: "Response");
        }
    }
}
