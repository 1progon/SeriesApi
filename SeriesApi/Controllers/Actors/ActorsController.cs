using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Data;
using SeriesApi.Dto.Actors;
using SeriesApi.Models.Actors;

namespace SeriesApi.Controllers.Actors
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ActorsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors(
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 28
        )
        {
            // generate cache name
            var cacheName = $"actors-index-offset-{offset}-limit-{limit}";

            // cache path
            var cacheDir = $"{_env.ContentRootPath}/storage/cache/actors";
            var cachePath = $"{cacheDir}/{cacheName}.json";

            // check if cache exists
            // todo move cache to separate service
            if (System.IO.File.Exists(cachePath))
            {
                // check file is expire
                if (System.IO.File.GetLastWriteTimeUtc(cachePath).AddDays(3) < DateTime.UtcNow)
                {
                    //  remove file
                    System.IO.File.Delete(cachePath);
                }
                else
                {
                    // read cache file
                    await using (var read = new FileStream(cachePath, FileMode.Open))
                    {
                        var actorsFromCache = await JsonSerializer.DeserializeAsync<List<ActorDto>>(read);
                        read.Close();

                        if (actorsFromCache is not null)
                        {
                            return actorsFromCache;
                        }
                    }
                }
            }


            // get data from db
            var actors = await _context.Actors
                .OrderBy(a => a.Name)
                .Skip(offset)
                .Take(limit)
                .Select(a => new ActorDto
                {
                    Name = a.Name,
                    Slug = a.Slug,
                    MainThumb = a.MainThumb
                })
                .ToListAsync();

            // save data to file for cache
            await using (var cacheFile = new FileStream(cachePath, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(cacheFile, actors, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                cacheFile.Close();
            }


            return actors;
        }

        // GET: api/Actors/slug-actor
        [HttpGet("{slug}")]
        public async Task<ActionResult<Actor>> GetActor(
            string slug,
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 28
        )
        {
            if (!await _context.Actors.AnyAsync()) return NotFound();

            var actor = await _context.Actors
                .Include(a => a.Movies
                    .OrderBy(m => m.Name)
                    .Skip(offset)
                    .Take(limit))
                .SingleOrDefaultAsync(a => a.Slug == slug);

            if (actor == null || offset >= limit && !actor.Movies.Any())
                return NotFound();


            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            if (_context.Actors == null)
            {
                return Problem("Entity set 'AppDbContext.Actors'  is null.");
            }

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            if (_context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActorExists(int id)
        {
            return (_context.Actors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}