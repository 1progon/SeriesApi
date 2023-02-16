using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Data;
using SeriesApi.Models.Middle;
using SeriesApi.Models.Movies;

namespace SeriesApi.Controllers.UserAccount
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserFavoriteMoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserFavoriteMoviesController(AppDbContext context) => _context = context;

        /* Get User Favorite Movies List */
        [HttpGet]
        public async Task<ActionResult<IList<Movie>>> GetFavoritesMovies()
        {
            Guid.TryParse(User.Identity?.Name, out var guid);


            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Guid == guid);

            if (user is null) return Unauthorized();

            var favorites = await _context
                .UserMovieFavorites
                .Include(f => f.Movie)
                .Where(f => f.UserId == user.Id)
                .Select(f => f.Movie)
                .ToListAsync();


            return favorites;
        }

        /* Add movie to user favorite list */
        [HttpPost("{id:int}")]
        public async Task<ActionResult> AddMovieToList([FromRoute] int id)
        {
            Guid.TryParse(User.Identity?.Name, out var guid);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Guid == guid);
            if (user is null) return Unauthorized();

            await _context.UserMovieFavorites
                .AddAsync(new UserFavoriteMovie { MovieId = id, UserId = user.Id });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (Convert.ToInt32(e.InnerException?.Data["SqlState"]) == 23505)
                {
                    return Conflict(new { Message = "Уже добавлено" });
                }

                return BadRequest(new { e.Message });
            }


            return NoContent();
        }

        /* Remove Movie from User Favorite List */
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> RemoveMovieFromList([FromRoute] int id)
        {
            Guid.TryParse(User.Identity?.Name, out var guid);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Guid == guid);
            if (user is null) return Unauthorized();


            _context.UserMovieFavorites
                .Remove(new UserFavoriteMovie { MovieId = id, UserId = user.Id });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Message = "Возможно данные не существуют" });
            }

            return NoContent();
        }
    }
}