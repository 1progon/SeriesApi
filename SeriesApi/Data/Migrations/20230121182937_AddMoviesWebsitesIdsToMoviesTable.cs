using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoviesWebsitesIdsToMoviesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImdbId",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KinopoiskId",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastEpisode",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastSeason",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MdlId",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShikimoriId",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorldartLink",
                table: "Movies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "KinopoiskId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LastEpisode",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LastSeason",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MdlId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ShikimoriId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "WorldartLink",
                table: "Movies");
        }
    }
}
