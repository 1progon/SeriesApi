namespace SeriesApi.Dto.Users;

public class UpdateUserDto
{
    public string Email { get; set; } = null!;
    
    public string Guid { get; set; } = null!;

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

    public string Token { get; set; } = null!;

    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
}