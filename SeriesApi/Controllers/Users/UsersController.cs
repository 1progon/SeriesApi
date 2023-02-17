using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Data;
using SeriesApi.Dto.Users;
using SeriesApi.Models.Users;

namespace SeriesApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context) => _context = context;


        [HttpPut("Update-User")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<UserDto>> UpdateUser(
            [FromBody] UpdateUserDto dto)
        {
            Guid.TryParse(User.Identity?.Name, out var guid);

            var user = await _context
                .Users
                .SingleOrDefaultAsync(u => u.Guid == guid);

            if (user == null) return NotFound();

            user.Email = dto.Email;
            user.BirthDate = dto.BirthDate;
            user.City = dto.City;
            user.Country = dto.Country;
            user.FirstName = dto.FirstName;
            user.Gender = dto.Gender;
            user.LastName = dto.LastName;
            user.OkProfile = dto.OkProfile;
            user.Phone = dto.Phone;
            user.Region = dto.Region;
            user.VkProfile = dto.VkProfile;


            _context.Users.Update(user);
            await _context.SaveChangesAsync();


            return UserDtoMapping(user);
        }

        // GET: api/Auth
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
                return NotFound();
            return await _context.Users.ToListAsync();
        }

        // GET: api/Auth/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public static UserDto UserDtoMapping(User user, string? token = null)
        {
            return new UserDto
            {
                Email = user.Email,
                Guid = user.Guid,
                BirthDate = user.BirthDate,
                City = user.City,
                Country = user.Country,
                FirstName = user.FirstName,
                Gender = user.Gender,
                LastName = user.LastName,
                OkProfile = user.OkProfile,
                Phone = user.Phone,
                Region = user.Region,
                VkProfile = user.VkProfile,
                Type = user.Type,
                Token = token,
            };
        }
    }
}