using System.ComponentModel.DataAnnotations;

namespace SeriesApi.Models.Movies;

public class MovieSeason
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }

    public int SeasonNumber { get; set; }

    public MovieVideo MovieVideo { get; set; } = null!;
    public int MovieVideoId { get; set; }

    public string? KodikLink { get; set; }

    public IList<MovieEpisode> Episodes { get; set; } = new List<MovieEpisode>();
}