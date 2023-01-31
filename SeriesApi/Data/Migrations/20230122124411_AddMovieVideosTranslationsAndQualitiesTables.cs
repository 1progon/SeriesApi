using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SeriesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieVideosTranslationsAndQualitiesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEpisodes_Movies_MovieId",
                table: "MovieEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieSeasons_Movies_MovieId",
                table: "MovieSeasons");

            migrationBuilder.DropIndex(
                name: "IX_MovieSeasons_MovieId",
                table: "MovieSeasons");

            migrationBuilder.DropColumn(
                name: "Article",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "EngName",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LastEpisode",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LastSeason",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MoviePath",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MovieSeasons",
                newName: "SeasonNumber");

            migrationBuilder.RenameColumn(
                name: "YouTubeTrailer",
                table: "Movies",
                newName: "TrailersString");

            migrationBuilder.RenameColumn(
                name: "Hidden",
                table: "Movies",
                newName: "HiddenMovie");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MovieEpisodes",
                newName: "MovieVideoId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieEpisodes_MovieId",
                table: "MovieEpisodes",
                newName: "IX_MovieEpisodes_MovieVideoId");

            migrationBuilder.AddColumn<string>(
                name: "KodikLink",
                table: "MovieSeasons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieVideoId",
                table: "MovieSeasons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Qualities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KodikTranslationId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    TranslationId = table.Column<int>(type: "integer", nullable: false),
                    QualityId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastSeason = table.Column<int>(type: "integer", nullable: true),
                    LastEpisode = table.Column<int>(type: "integer", nullable: true),
                    SeasonsCount = table.Column<int>(type: "integer", nullable: true),
                    EpisodesCount = table.Column<int>(type: "integer", nullable: true),
                    KodikMovieId = table.Column<string>(type: "text", nullable: true),
                    KodikLink = table.Column<string>(type: "text", nullable: true),
                    HiddenVideo = table.Column<bool>(type: "boolean", nullable: false),
                    Camrip = table.Column<bool>(type: "boolean", nullable: false),
                    Lgbt = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieVideos_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieVideos_Qualities_QualityId",
                        column: x => x.QualityId,
                        principalTable: "Qualities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieVideos_Translations_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieSeasons_MovieVideoId",
                table: "MovieSeasons",
                column: "MovieVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieVideos_MovieId",
                table: "MovieVideos",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieVideos_QualityId",
                table: "MovieVideos",
                column: "QualityId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieVideos_TranslationId",
                table: "MovieVideos",
                column: "TranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_Slug",
                table: "Translations",
                column: "Slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEpisodes_MovieVideos_MovieVideoId",
                table: "MovieEpisodes",
                column: "MovieVideoId",
                principalTable: "MovieVideos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieSeasons_MovieVideos_MovieVideoId",
                table: "MovieSeasons",
                column: "MovieVideoId",
                principalTable: "MovieVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEpisodes_MovieVideos_MovieVideoId",
                table: "MovieEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieSeasons_MovieVideos_MovieVideoId",
                table: "MovieSeasons");

            migrationBuilder.DropTable(
                name: "MovieVideos");

            migrationBuilder.DropTable(
                name: "Qualities");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropIndex(
                name: "IX_MovieSeasons_MovieVideoId",
                table: "MovieSeasons");

            migrationBuilder.DropColumn(
                name: "KodikLink",
                table: "MovieSeasons");

            migrationBuilder.DropColumn(
                name: "MovieVideoId",
                table: "MovieSeasons");

            migrationBuilder.RenameColumn(
                name: "SeasonNumber",
                table: "MovieSeasons",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "TrailersString",
                table: "Movies",
                newName: "YouTubeTrailer");

            migrationBuilder.RenameColumn(
                name: "HiddenMovie",
                table: "Movies",
                newName: "Hidden");

            migrationBuilder.RenameColumn(
                name: "MovieVideoId",
                table: "MovieEpisodes",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieEpisodes_MovieVideoId",
                table: "MovieEpisodes",
                newName: "IX_MovieEpisodes_MovieId");

            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngName",
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
                name: "MoviePath",
                table: "Movies",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieSeasons_MovieId",
                table: "MovieSeasons",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEpisodes_Movies_MovieId",
                table: "MovieEpisodes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieSeasons_Movies_MovieId",
                table: "MovieSeasons",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
