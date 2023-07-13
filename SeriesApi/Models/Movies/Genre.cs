namespace SeriesApi.Models.Movies;

public class Genre : BaseModel
{
    public IList<Movie> Movies { get; set; } = new List<Movie>();
}