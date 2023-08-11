using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Data;
using SeriesApi.Dto.Movies;
using SeriesApi.Models.Movies;

namespace SeriesApi.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public GenresController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            if (!await _context.Genres.AnyAsync()) return NotFound();

            // cache file path
            var cacheFolder = $"{_environment.ContentRootPath}/storage/cache/genres";
            var cachePath = $"{cacheFolder}/genres.json";

            // check cache file exists
            if (System.IO.File.Exists(cachePath))
            {
                var genresFromCache
                    = JsonSerializer
                        .Deserialize<List<GenreDto>>(await System.IO.File.ReadAllBytesAsync(cachePath));

                if (genresFromCache is not null && genresFromCache.Count > 0)
                    return genresFromCache;


                // empty file or empty list - remove
                System.IO.File.Delete(cachePath);
            }

            // if no cache
            var genres = await _context.Genres.Select(g => new GenreDto
            {
                Name = g.Name,
                Slug = g.Slug
            }).ToListAsync();

            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cacheFolder);
            }

            // save data to cache file
            var json = JsonSerializer.Serialize(genres, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            await System.IO.File.WriteAllTextAsync(cachePath, json);


            return genres;
        }

        // GET: api/Genres/slug-name
        [HttpGet("{slug}")]
        public async Task<ActionResult<GenreShowDto>> GetGenre(
            [FromRoute] string slug,
            [FromQuery] int limit = 28,
            [FromQuery] int offset = 0)
        {
            var genre = await _context.Genres
                .Select(g => new GenreShowDto
                {
                    Name = g.Name,
                    Slug = g.Slug,
                    Movies = (IList<MovieDto>)g.Movies
                        .Skip(offset)
                        .Take(limit)
                        .Select(m => new MovieDto
                        {
                            Name = m.Name,
                            Slug = m.Slug,
                            MainImageThumb = m.MainImageThumb,
                            Year = m.Year,
                            Rating = m.Rating,
                            SeasonsCount = m.SeasonsCount,
                            EpisodesCount = m.EpisodesCount,
                            CommentsCount = m.Comments != null ? m.Comments.Count : null
                        })
                })
                .SingleOrDefaultAsync(g => g.Slug == slug);


            if (genre == null || offset >= limit && !genre.Movies.Any())
            {
                return NotFound();
            }

            return genre;
        }

        // PUT: api/Genres/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genres
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            if (!await _context.Genres.AnyAsync())
            {
                return Problem("Entity set 'AppDbContext.Genres'  is null.");
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenre", new { id = genre.Id }, genre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (!await _context.Genres.AnyAsync())
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}