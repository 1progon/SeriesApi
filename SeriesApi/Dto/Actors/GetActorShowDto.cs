using SeriesApi.Models.Actors;
using SeriesApi.Models.Movies;

namespace SeriesApi.Dto.Actors;

public class GetActorShowDto
{
    public Actor Actor { get; set; } = null!;
    public IList<Movie>? Movies { get; set; }
}