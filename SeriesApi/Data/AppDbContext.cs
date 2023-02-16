using Microsoft.EntityFrameworkCore;
using SeriesApi.Models.Actors;
using SeriesApi.Models.Middle;
using SeriesApi.Models.Movies;
using SeriesApi.Models.Users;

namespace SeriesApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    // movie
    public DbSet<Movie> Movies { get; set; } = null!;

    public DbSet<MovieVideo> MovieVideos { get; set; } = null!;
    public DbSet<MovieSeason> MovieSeasons { get; set; } = null!;

    // todo set unique index column (seriesNumber + translate + movieId + season)
    public DbSet<MovieEpisode> MovieEpisodes { get; set; } = null!;
    
    // movies-users many to many
    public DbSet<UserFavoriteMovie> UserMovieFavorites { get; set; } = null!;
    public DbSet<UserMovieLikeDislike> UserMovieLikeDislikes { get; set; } = null!;

    // translations movie
    public DbSet<Translation> Translations { get; set; } = null!;

    // qualities
    public DbSet<Quality> Qualities { get; set; } = null!;

    // movie collections
    public DbSet<Collection> Collections { get; set; } = null!;

    // movie anthology
    public DbSet<Anthology> Anthologies { get; set; } = null!;

    // movie comments
    public DbSet<Comment> Comments { get; set; } = null!;

    // tag
    public DbSet<Tag> Tags { get; set; } = null!;

    // movie genres
    public DbSet<Genre> Genres { get; set; } = null!;

    // actors
    public DbSet<Actor> Actors { get; set; } = null!;

    // users
    public DbSet<User> Users { get; set; } = null!;
}