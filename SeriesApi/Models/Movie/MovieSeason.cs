using System.ComponentModel.DataAnnotations;

namespace SeriesApi.Models.Movie;

public class MovieSeason
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }

    public Movie Movie { get; set; } = null!;
    public int MovieId { get; set; }

    public IList<MovieEpisode>? Episodes { get; set; }
}