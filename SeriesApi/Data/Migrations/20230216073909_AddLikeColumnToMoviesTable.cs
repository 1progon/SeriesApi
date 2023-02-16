using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLikeColumnToMoviesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Movies");
        }
    }
}
