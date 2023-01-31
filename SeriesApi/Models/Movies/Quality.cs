using System.ComponentModel.DataAnnotations;

namespace SeriesApi.Models.Movies;

public class Quality
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = null!;
}