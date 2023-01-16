namespace SeriesApi.Models.Movie;

public class Actor : BaseModel
{
    public IList<Movie>? Movies { get; set; }
}