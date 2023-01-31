using SeriesApi.Enums.Movies;
using SeriesApi.Models.Actors;

namespace SeriesApi.Models.Movies;

public class Movie : BaseModel
{
    public MovieType Type { get; set; }

    public string? OtherNames { get; set; }

    public string? Description { get; set; }

    public string? MainImage { get; set; }
    public string? MainImageThumb { get; set; }

    public int Year { get; set; }
    public string? PremierDate { get; set; }

    public string? CountryString { get; set; }

    public float? Rating { get; set; }
    public int? RatingCount { get; set; }

    public int? SeasonsCount { get; set; }
    public int? EpisodesCount { get; set; }

    public IList<MovieVideo> MovieVideos { get; set; } = new List<MovieVideo>();
    public IList<Genre>? Genres { get; set; }
    public IList<Tag>? Tags { get; set; }

    public IList<Actor>? Actors { get; set; }

    public Anthology Anthology { get; set; } = new();
    public int? AnthologyId { get; set; }

    public IList<Comment>? Comments { get; set; }

    public string? LinkParsedFrom { get; set; }

    public string? ImagesString { get; set; }
    public string? TrailersString { get; set; }

    public IList<Collection>? Collections { get; set; }

    public bool Popular { get; set; }
    public bool EditorChoice { get; set; }
    public bool Soon { get; set; }
    public bool HiddenMovie { get; set; }

    public string? KinopoiskId { get; set; }
    public string? ImdbId { get; set; }
    public string? MdlId { get; set; }
    public string? ShikimoriId { get; set; }
    public string? WorldartLink { get; set; }
}