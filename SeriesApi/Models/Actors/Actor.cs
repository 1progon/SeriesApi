using SeriesApi.Models.Movies;

namespace SeriesApi.Models.Actors;

public class Actor : BaseModel
{
    public string? MainImage { get; set; }
    public string? MainThumb { get; set; }
    public IList<Movie> Movies { get; set; } = new List<Movie>();
}