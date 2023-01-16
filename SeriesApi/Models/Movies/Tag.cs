namespace SeriesApi.Models.Movies;

public class Tag : BaseModel
{
    public IList<Movie>? Movies { get; set; }
}