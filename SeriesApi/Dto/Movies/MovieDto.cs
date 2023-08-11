using System.Text.Json.Serialization;

namespace SeriesApi.Dto.Movies;

public class MovieDto : BaseDto
{
    [JsonPropertyName("mainImageThumb")] public string? MainImageThumb { get; set; }

    [JsonPropertyName("year")] public int Year { get; set; }

    [JsonPropertyName("rating")] public float? Rating { get; set; }

    [JsonPropertyName("seasonsCount")] public int? SeasonsCount { get; set; }
    [JsonPropertyName("episodesCount")] public int? EpisodesCount { get; set; }

    [JsonPropertyName("commentsCount")] public int? CommentsCount { get; set; }
}