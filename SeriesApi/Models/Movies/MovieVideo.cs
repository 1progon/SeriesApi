using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SeriesApi.Models.Movies;

[Index(nameof(KodikMovieId), IsUnique = true)]
public class MovieVideo
{
    [Key] public int Id { get; set; }

    public Movie Movie { get; set; } = null!;
    public int MovieId { get; set; }

    public Translation Translation { get; set; } = null!;
    public int TranslationId { get; set; }

    public Quality Quality { get; set; } = null!;
    public int QualityId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int? LastSeason { get; set; }
    public int? LastEpisode { get; set; }

    public int? SeasonsCount { get; set; }
    public int? EpisodesCount { get; set; }


    public string? KodikMovieId { get; set; }

    public string? KodikLink { get; set; }

    public IList<MovieSeason> Seasons { get; set; } = new List<MovieSeason>();
    // public IList<MovieEpisode>? Episodes { get; set; }

    public bool HiddenVideo { get; set; }
    public bool Camrip { get; set; }
    public bool Lgbt { get; set; }
}