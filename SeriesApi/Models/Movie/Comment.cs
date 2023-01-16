using System.ComponentModel.DataAnnotations;

namespace SeriesApi.Models.Movie;

public class Comment
{
    [Key] public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;

    public float Rating { get; set; }
    public int RatingCount { get; set; }

    public Movie Movie { get; set; } = null!;
    public int MovieId { get; set; }

    public User.User Author { get; set; } = null!;
    public int AuthorId { get; set; }
}