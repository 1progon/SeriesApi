using System.ComponentModel.DataAnnotations;

namespace SeriesApi.Models.Movies;

public class MovieEpisode
{
    [Key] public int Id { get; set; }

    public string? Name { get; set; } = null!;

    public int SeriesNumber { get; set; }

    public MovieSeason? Season { get; set; }
    public int? SeasonId { get; set; }

    public MovieVideo? Movie { get; set; }
    public int? MovieVideoId { get; set; }

    public string? KodikLink { get; set; }
}