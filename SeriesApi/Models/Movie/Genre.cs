namespace SeriesApi.Models.Movie;

public class Genre : BaseModel
{
    public IList<Movie>? Movies { get; set; }
}