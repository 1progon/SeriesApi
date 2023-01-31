using System.Collections.ObjectModel;

namespace SeriesApi.Models.Movies;

public class Collection : BaseModel
{
    public string? Thumb { get; set; }
    public IList<Movie> Movies { get; set; } = new Collection<Movie>();
}