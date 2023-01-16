using System.ComponentModel.DataAnnotations;

namespace SeriesApi.Models.Users;

public class User
{
    [Key] public int Id { get; set; }
    public Guid Guid { get; set; }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string? Token { get; set; }
    public string? TokenExpire { get; set; }
}