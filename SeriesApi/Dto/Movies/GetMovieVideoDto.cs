using SeriesApi.Models.Movies;

namespace SeriesApi.Dto.Movies;

public class GetMovieVideoDto
{
    public MovieVideo Video { get; set; } = null!;
    public IList<MovieVideo> OtherMovieVideos { get; set; } = new List<MovieVideo>();
}