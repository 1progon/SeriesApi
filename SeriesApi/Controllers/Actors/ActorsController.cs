using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Data;
using SeriesApi.Models.Actors;

namespace SeriesApi.Controllers.Actors
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActorsController(AppDbContext context) => _context = context;

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors(
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 28
        )
        {
            return await _context.Actors
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
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

            if (actor == null) return NotFound();


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