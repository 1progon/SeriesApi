using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SeriesApi.Controllers.Users;
using SeriesApi.Data;
using SeriesApi.Dto.Auth;
using SeriesApi.Dto.Users;
using SeriesApi.Enums.Users;
using SeriesApi.Models.Users;

namespace SeriesApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string _generateToken(User user)
        {
            var now = DateTime.UtcNow;

            var k = _configuration.GetSection("Jwt")["Key"];
            if (k is null) return string.Empty;

            var key = Encoding.ASCII.GetBytes(k);

            var claims = new List<Claim>()
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Guid.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Type.ToString()),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt")["Issuer"],
                audience: _configuration.GetSection("Jwt")["Audience"],
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(30),
                signingCredentials: signingCredentials);


            var t = new JwtSecurityTokenHandler().WriteToken(token);

            return t;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(
            [FromBody] LoginFormDto dto)
        {
            var user = await _context
                .Users
                .SingleOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null)
                return Unauthorized(new { Message = "Пользователь не найден" });

            var hasher = new PasswordHasher<User>();
            var passwordHash = hasher
                .VerifyHashedPassword(user,
                    user.Password, dto.Password);

            if (passwordHash == PasswordVerificationResult.Failed)
                return Unauthorized(new { Message = "Возможно пароль неверный" });


            var token = _generateToken(user);


            return UsersController.UserDtoMapping(user, token);
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(
            [FromBody] RegisterFormDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (userExists) return Conflict(new { Message = "Пользователь уже существует" });

            if (dto.Password != dto.PasswordConfirm)
                return BadRequest(new { Message = "Пароли должны совпадать" });

            var user = new User
            {
                Email = dto.Email,
                Guid = Guid.NewGuid(),
                Type = UserType.User,
            };

            var hashPassword = new PasswordHasher<User>()
                .HashPassword(user, dto.Password);

            user.Password = hashPassword;


            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = _generateToken(user);

            return UsersController.UserDtoMapping(user, token);
        }
    }
}