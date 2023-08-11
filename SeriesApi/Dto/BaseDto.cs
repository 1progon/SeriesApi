using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SeriesApi.Dto;

public class BaseDto
{
    [JsonPropertyName("name")] [Required] public string Name { get; set; } = null!;
    [JsonPropertyName("slug")] [Required] public string Slug { get; set; } = null!;
}