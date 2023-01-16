using System.ComponentModel.DataAnnotations;

namespace SeriesApi.Models.Movie;

public class MovieEpisode : BaseModel
{
    [Key] public override int Id { get; set; }

    public int SeriesNumber { get; set; }

    public MovieSeason? Season { get; set; }
    public int? SeasonId { get; set; }

    public Movie? Movie { get; set; }
    public int? MovieId { get; set; }
}