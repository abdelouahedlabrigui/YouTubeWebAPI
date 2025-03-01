using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class WeatherFifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeatherVisualizationId",
                table: "WindSpeeds");

            migrationBuilder.DropColumn(
                name: "WeatherVisualizationId",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "WeatherVisualizationId",
                table: "Humidities");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WindSpeeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "WindSpeeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Temperatures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Temperatures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Humidities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Humidities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "WindSpeeds");

            migrationBuilder.DropColumn(
                name: "State",
                table: "WindSpeeds");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Humidities");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Humidities");

            migrationBuilder.AddColumn<int>(
                name: "WeatherVisualizationId",
                table: "WindSpeeds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeatherVisualizationId",
                table: "Temperatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeatherVisualizationId",
                table: "Humidities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
