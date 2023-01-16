using Microsoft.EntityFrameworkCore;
using SeriesApi.Models.Movie;
using SeriesApi.Models.User;

namespace SeriesApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    // movie
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<MovieSeason> MovieSeasons { get; set; } = null!;
    public DbSet<MovieEpisode> MovieEpisodes { get; set; } = null!;

    // anthology
    public DbSet<Anthology> Anthologies { get; set; } = null!;

    // comments
    public DbSet<Comment> Comments { get; set; } = null!;

    // tag
    public DbSet<Tag> Tags { get; set; } = null!;

    // genre
    public DbSet<Genre> Genres { get; set; } = null!;

    // actors
    public DbSet<Actor> Actors { get; set; } = null!;

    // users
    public DbSet<User> Users { get; set; } = null!;
}