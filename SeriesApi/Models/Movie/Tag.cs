namespace SeriesApi.Models.Movie;

public class Tag : BaseModel
{
    public IList<Movie>? Movies { get; set; }
}