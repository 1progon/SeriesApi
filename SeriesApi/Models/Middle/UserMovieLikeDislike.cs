using Microsoft.EntityFrameworkCore;
using SeriesApi.Enums.Movies;
using SeriesApi.Models.Movies;
using SeriesApi.Models.Users;

namespace SeriesApi.Models.Middle;

[PrimaryKey(nameof(MovieId), nameof(UserId))]
public class UserMovieLikeDislike
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public MovieLikeDislikeType Type { get; set; }
}