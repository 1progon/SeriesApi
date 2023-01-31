using SeriesApi.Models.Movies;

namespace SeriesApi.Dto.Movies;

public class GetMoviesShowDto
{
    public Movie Movie { get; set; } = null!;
    public Anthology? Anthology { get; set; }
}