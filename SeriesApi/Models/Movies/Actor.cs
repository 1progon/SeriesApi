namespace SeriesApi.Models.Movies;

public class Actor : BaseModel
{
    public IList<Movie>? Movies { get; set; }
}