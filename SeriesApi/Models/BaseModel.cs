using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SeriesApi.Models;

[Index(nameof(Slug), IsUnique = true)]
public abstract class BaseModel
{
    [Key] public virtual int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    [Required] public string Slug { get; set; } = null!;
}