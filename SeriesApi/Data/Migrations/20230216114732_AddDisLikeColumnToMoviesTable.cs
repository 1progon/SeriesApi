﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDisLikeColumnToMoviesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisLikes",
                table: "Movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisLikes",
                table: "Movies");
        }
    }
}
