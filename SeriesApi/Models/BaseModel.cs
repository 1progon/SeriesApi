using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SeriesApi.Models;

[Index(nameof(Slug), IsUnique = true)]
public abstract class BaseModel
{
    [JsonPropertyName("id")][Key] public virtual int Id { get; set; }
    [JsonPropertyName("name")][Required] public string Name { get; set; } = null!;
    [JsonPropertyName("slug")][Required] public string Slug { get; set; } = null!;
}