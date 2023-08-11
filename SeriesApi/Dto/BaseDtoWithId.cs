using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SeriesApi.Dto;

public class BaseDtoWithId : BaseDto
{
    [JsonPropertyName("id")] [Key] public virtual int Id { get; set; }
}