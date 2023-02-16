using SeriesApi.Enums.Users;

namespace SeriesApi.Dto.Users;

public class UserDto
{
    public Guid Guid { get; set; }
    public string Email { get; set; } = null!;

    public string? Gender { get; set; }
    public string? BirthDate { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }

    public string? VkProfile { get; set; }
    public string? OkProfile { get; set; }

    public string? Token { get; set; }
    public int? TokenExpire { get; set; }

    public UserType Type { get; set; }
}