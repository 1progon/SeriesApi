using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsImagesInActorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Actors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainThumb",
                table: "Actors",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "MainThumb",
                table: "Actors");
        }
    }
}
