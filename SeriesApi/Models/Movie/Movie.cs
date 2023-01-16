using SeriesApi.Enums.Movie;

namespace SeriesApi.Models.Movie;

public class Movie : BaseModel
{
    public MovieType Type { get; set; }

    public string? OtherNames { get; set; }

    public string? EngName { get; set; }

    public string? Description { get; set; }
    public string? Article { get; set; }
    public string? MainImage { get; set; }
    public string? MainImageThumb { get; set; }

    public string? YouTubeTrailer { get; set; }
    public string? MoviePath { get; set; }

    public int Year { get; set; }
    public string? CountryString { get; set; }

    public int? SeasonsCount { get; set; }
    public int? EpisodesCount { get; set; }

    public float? Rating { get; set; }
    public int? RatingCount { get; set; }

    public IList<MovieSeason>? Seasons { get; set; }
    public IList<MovieEpisode>? Episodes { get; set; }

    public IList<Genre>? Genres { get; set; }
    public IList<Tag>? Tags { get; set; }

    public IList<Actor>? Actors { get; set; }

    public Anthology? Anthology { get; set; }
    public int? AnthologyId { get; set; }

    public IList<Comment>? Comments { get; set; }
}