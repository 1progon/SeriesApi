using Microsoft.EntityFrameworkCore;
using SeriesApi.Models.Movies;
using SeriesApi.Models.Users;

namespace SeriesApi.Models.Middle;

[PrimaryKey(nameof(MovieId), nameof(UserId))]
public class UserFavoriteMovie
{
    public Movie Movie { get; set; } = null!;
    public int MovieId { get; set; }


    public User User { get; set; } = null!;
    public int UserId { get; set; }
}