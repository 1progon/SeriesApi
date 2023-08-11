using System.Text.Json.Serialization;

namespace SeriesApi.Dto.Movies;

public class GenreShowDto : BaseDto
{
    [JsonPropertyName("movies")] public IList<MovieDto> Movies { get; set; } = new List<MovieDto>();
}