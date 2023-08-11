using System.Text.Json.Serialization;

namespace SeriesApi.Dto.Actors;

public class ActorDto : BaseDto
{
    [JsonPropertyName("mainThumb")] public string? MainThumb { get; set; }
}