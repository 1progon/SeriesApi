namespace SeriesApi.Models.Movie;

public class Anthology : BaseModel
{
    public IList<Movie>? Movies { get; set; }
}