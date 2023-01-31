﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SeriesApi.Data;

#nullable disable

namespace SeriesApi.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230124191625_AddUniqueForKodikMovieIdOnMovieVideosTable")]
    partial class AddUniqueForKodikMovieIdOnMovieVideosTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.Property<int>("ActorsId")
                        .HasColumnType("integer");

                    b.Property<int>("MoviesId")
                        .HasColumnType("integer");

                    b.HasKey("ActorsId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("ActorMovie");
                });

            modelBuilder.Entity("CollectionMovie", b =>
                {
                    b.Property<int>("CollectionsId")
                        .HasColumnType("integer");

                    b.Property<int>("MoviesId")
                        .HasColumnType("integer");

                    b.HasKey("CollectionsId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CollectionMovie");
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.Property<int>("MoviesId")
                        .HasColumnType("integer");

                    b.HasKey("GenresId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("GenreMovie");
                });

            modelBuilder.Entity("MovieTag", b =>
                {
                    b.Property<int>("MoviesId")
                        .HasColumnType("integer");

                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.HasKey("MoviesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("MovieTag");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MainImage")
                        .HasColumnType("text");

                    b.Property<string>("MainThumb")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Anthology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Anthologies");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Thumb")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("RatingCount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("MovieId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnthologyId")
                        .HasColumnType("integer");

                    b.Property<string>("CountryString")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("EditorChoice")
                        .HasColumnType("boolean");

                    b.Property<int?>("EpisodesCount")
                        .HasColumnType("integer");

                    b.Property<bool>("HiddenMovie")
                        .HasColumnType("boolean");

                    b.Property<string>("ImagesString")
                        .HasColumnType("text");

                    b.Property<string>("ImdbId")
                        .HasColumnType("text");

                    b.Property<string>("KinopoiskId")
                        .HasColumnType("text");

                    b.Property<string>("LinkParsedFrom")
                        .HasColumnType("text");

                    b.Property<string>("MainImage")
                        .HasColumnType("text");

                    b.Property<string>("MainImageThumb")
                        .HasColumnType("text");

                    b.Property<string>("MdlId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OtherNames")
                        .HasColumnType("text");

                    b.Property<bool>("Popular")
                        .HasColumnType("boolean");

                    b.Property<string>("PremierDate")
                        .HasColumnType("text");

                    b.Property<float?>("Rating")
                        .HasColumnType("real");

                    b.Property<int?>("RatingCount")
                        .HasColumnType("integer");

                    b.Property<int?>("SeasonsCount")
                        .HasColumnType("integer");

                    b.Property<string>("ShikimoriId")
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Soon")
                        .HasColumnType("boolean");

                    b.Property<string>("TrailersString")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("WorldartLink")
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnthologyId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.MovieEpisode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("KodikLink")
                        .HasColumnType("text");

                    b.Property<int?>("MovieVideoId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("integer");

                    b.Property<int>("SeriesNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MovieVideoId");

                    b.HasIndex("SeasonId");

                    b.ToTable("MovieEpisodes");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.MovieSeason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("KodikLink")
                        .HasColumnType("text");

                    b.Property<int>("MovieVideoId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("SeasonNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MovieVideoId");

                    b.ToTable("MovieSeasons");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.MovieVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Camrip")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EpisodesCount")
                        .HasColumnType("integer");

                    b.Property<bool>("HiddenVideo")
                        .HasColumnType("boolean");

                    b.Property<string>("KodikLink")
                        .HasColumnType("text");

                    b.Property<string>("KodikMovieId")
                        .HasColumnType("text");

                    b.Property<int?>("LastEpisode")
                        .HasColumnType("integer");

                    b.Property<int?>("LastSeason")
                        .HasColumnType("integer");

                    b.Property<bool>("Lgbt")
                        .HasColumnType("boolean");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("QualityId")
                        .HasColumnType("integer");

                    b.Property<int?>("SeasonsCount")
                        .HasColumnType("integer");

                    b.Property<int>("TranslationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("KodikMovieId")
                        .IsUnique();

                    b.HasIndex("MovieId");

                    b.HasIndex("QualityId");

                    b.HasIndex("TranslationId");

                    b.ToTable("MovieVideos");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Quality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Qualities");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Translation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("KodikTranslationId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("SeriesApi.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("TokenExpire")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.HasOne("SeriesApi.Models.Movies.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeriesApi.Models.Movies.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CollectionMovie", b =>
                {
                    b.HasOne("SeriesApi.Models.Movies.Collection", null)
                        .WithMany()
                        .HasForeignKey("CollectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeriesApi.Models.Movies.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.HasOne("SeriesApi.Models.Movies.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeriesApi.Models.Movies.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieTag", b =>
                {
                    b.HasOne("SeriesApi.Models.Movies.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeriesApi.Models.Movies.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Comment", b =>
                {
                    b.HasOne("SeriesApi.Models.Users.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeriesApi.Models.Movies.Movie", "Movie")
                        .WithMany("Comments")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Movie", b =>
                {
                    b.HasOne("SeriesApi.Models.Movies.Anthology", "Anthology")
                        .WithMany("Movies")
                        .HasForeignKey("AnthologyId");

                    b.Navigation("Anthology");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.MovieEpisode", b =>
                {
                    b.HasOne("SeriesApi.Models.Movies.MovieVideo", "Movie")
                        .WithMany("Episodes")
                        .HasForeignKey("MovieVideoId");

                    b.HasOne("SeriesApi.Models.Movies.MovieSeason", "Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId");

                    b.Navigation("Movie");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.MovieSeason", b =>
                {
                    b.HasOne("SeriesApi.Models.Movies.MovieVideo", "MovieVideo")
                        .WithMany("Seasons")
                        .HasForeignKey("MovieVideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieVideo");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.MovieVideo", b =>
                {
                    b.HasOne("SeriesApi.Models.Movies.Movie", "Movie")
                        .WithMany("MovieVideos")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeriesApi.Models.Movies.Quality", "Quality")
                        .WithMany()
                        .HasForeignKey("QualityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeriesApi.Models.Movies.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("TranslationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Quality");

                    b.Navigation("Translation");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Anthology", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.Movie", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("MovieVideos");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.MovieSeason", b =>
                {
                    b.Navigation("Episodes");
                });

            modelBuilder.Entity("SeriesApi.Models.Movies.MovieVideo", b =>
                {
                    b.Navigation("Episodes");

                    b.Navigation("Seasons");
                });
#pragma warning restore 612, 618
        }
    }
}
