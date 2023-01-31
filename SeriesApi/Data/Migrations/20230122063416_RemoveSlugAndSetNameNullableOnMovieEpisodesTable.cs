using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSlugAndSetNameNullableOnMovieEpisodesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieEpisodes_Slug",
                table: "MovieEpisodes");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "MovieEpisodes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MovieEpisodes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MovieEpisodes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "MovieEpisodes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MovieEpisodes_Slug",
                table: "MovieEpisodes",
                column: "Slug",
                unique: true);
        }
    }
}
