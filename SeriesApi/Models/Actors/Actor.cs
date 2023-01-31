using SeriesApi.Models.Movies;

namespace SeriesApi.Models.Actors;

public class Actor : BaseModel
{
    public override int Id { get; set; }
    
    public string? MainImage { get; set; }
    public string? MainThumb { get; set; }
    public IList<Movie> Movies { get; set; } = new List<Movie>();
}