namespace SeriesApi.Dto.Auth;

public class LoginFormDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}