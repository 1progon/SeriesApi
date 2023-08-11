using System.Text.Json.Serialization;
using SeriesApi.Dto.Movies;

namespace SeriesApi.Dto.Actors;

public class ActorShowDto : BaseDto
{
    [JsonPropertyName("mainThumb")] public string? MainThumb { get; set; }
    [JsonPropertyName("movies")] public IList<MovieDto> Movies { get; set; } = new List<MovieDto>();
}