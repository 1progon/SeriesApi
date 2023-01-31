using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Data;
using SeriesApi.Models.Movies;

namespace SeriesApi.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CollectionsController(AppDbContext context) => _context = context;

        // GET: api/Collections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollections()
        {
            if (!await _context.Collections.AnyAsync()) return NotFound();

            return await _context.Collections.ToListAsync();
        }

        // GET: api/Collections/slug-name
        [HttpGet("{slug}")]
        public async Task<ActionResult<Collection>> GetCollection(
            [FromRoute] string slug,
            [FromQuery] int limit = 28,
            [FromQuery] int offset = 0)
        {
            if (!await _context.Collections.AnyAsync()) return NotFound();

            var collection = await _context.Collections
                .Include(c => c.Movies
                    .OrderBy(m => m.Name)
                    .Skip(offset)
                    .Take(limit))
                .SingleOrDefaultAsync(c => c.Slug == slug);

            if (collection == null) return NotFound();

            return collection;
        }

        // PUT: api/Collections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCollection(int id, Collection collection)
        {
            if (id != collection.Id)
            {
                return BadRequest();
            }

            _context.Entry(collection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(id))
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

        // POST: api/Collections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Collection>> PostCollection(Collection collection)
        {
            if (_context.Collections == null)
            {
                return Problem("Entity set 'AppDbContext.Collections'  is null.");
            }

            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollection", new { id = collection.Id }, collection);
        }

        // DELETE: api/Collections/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            if (_context.Collections == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollectionExists(int id)
        {
            return (_context.Collections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}