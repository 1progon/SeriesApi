namespace SeriesApi.Models.Movies;

public class Anthology : BaseModel
{
    public IList<Movie>? Movies { get; set; }
}