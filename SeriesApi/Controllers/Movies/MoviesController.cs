using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Data;
using SeriesApi.Dto.Movies;
using SeriesApi.Enums.Movies;
using SeriesApi.Models.Middle;
using SeriesApi.Models.Movies;

namespace SeriesApi.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;


        public MoviesController(AppDbContext context) => _context = context;

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies(
            [FromQuery] MoviesSelector? selector,
            [FromQuery] string? search,
            [FromQuery] int limit = 28,
            [FromQuery] int offset = 0
        )
        {
            var query = _context.Movies
                .Where(m => !m.HiddenMovie);

            switch (selector)
            {
                case MoviesSelector.New:
                    query = query
                        .OrderByDescending(m => m.Year)
                        .Where(m => m.Year >= DateTime.UtcNow.Year - 1);
                    break;
                case MoviesSelector.Popular:
                    query = query.OrderByDescending(m => m.RatingCount)
                        .Where(m => m.Popular);
                    break;
                case MoviesSelector.Soon:
                    // todo change with soon
                    query = query.OrderBy(m => m.PremierDate)
                        .Where(m => m.Year >= DateTime.UtcNow.Year)
                        .Where(m => m.Soon);
                    break;
                case MoviesSelector.Best:
                    query = query.OrderByDescending(m => m.Rating);
                    break;
                case MoviesSelector.Choice:
                    query = query.OrderByDescending(m => m.RatingCount)
                        .Where(m => m.EditorChoice);
                    break;
                case MoviesSelector.Random:
                    var r = new Random()
                        .Next(1, await _context.Movies
                            .MaxAsync(m => m.Id));

                    query = query.OrderBy(m => m.Id)
                        .Where(m => m.Id == r);
                    break;
                default:
                    query = query.OrderBy(m => m.Name);
                    break;
            }

            if (search is not null)
            {
                search = search.ToLower();
                query = query
                    .Where(m => m.Name.ToLower().Contains(search)
                                || m.Slug.ToLower().Contains(search)
                                || (m.OtherNames != null && m.OtherNames
                                    .ToLower().Contains(search)));
            }

            query = query.Skip(offset).Take(limit);

            var movies = await query.ToListAsync();


            if (offset >= limit && movies.Count < 1) return NotFound();

            return movies;
        }

        // GET: api/Movies/slug-name
        [HttpGet("{slug}")]
        public async Task<ActionResult<GetMoviesShowDto>> GetMovie([FromRoute] string slug)
        {
            if (!await _context.Movies.AnyAsync()) return NotFound();

            var movieQ = _context.Movies.AsQueryable();

            movieQ = movieQ.Include(m => m.MovieVideos
                    .OrderBy(mv => mv.Translation.Type)
                    .ThenByDescending(mv => mv.Seasons.Count)
                    .Take(1))
                .ThenInclude(mv => mv.Seasons
                    .OrderBy(ms => ms.SeasonNumber)
                    .Take(1))
                .ThenInclude(ms => ms.Episodes
                    .OrderBy(me => me.SeriesNumber)
                    .Take(1))
                .Include(m => m.Actors)
                .Include(m => m.Genres)
                .Include(m => m.Tags)
                .Include(m => m.Comments)
                .Include(m => m.Collections)
                .Include(m => m.Anthology)
                .ThenInclude(a => a.Movies);


            if (User.Identity is { IsAuthenticated: true })
            {
                Guid.TryParse(User.Identity?.Name, out var guid);

                if (guid != Guid.Empty)
                {
                    movieQ = movieQ
                        .Include(m => m.UsersLikes
                            .Where(l => l.Movie.Slug == slug
                                        && l.User.Guid == guid));


                    movieQ = movieQ
                        .Include(m => m.UsersFavorites
                            .Where(f => f.Movie.Slug == slug
                                        && f.User.Guid == guid));
                }
            }


            var movie = await movieQ
                .OrderBy(m => m.Year)
                .SingleOrDefaultAsync(m => m.Slug == slug);

            if (movie == null) return NotFound();

            return new GetMoviesShowDto()
            {
                Anthology = movie.Anthology,
                Movie = movie
            };
        }

        // GET: api/MovieVideo/slug-name
        [HttpGet("/api/MovieVideo/{movieSlug}")]
        public async Task<ActionResult<GetMovieVideoDto>> GetMovieVideo(
            [FromRoute] string movieSlug,
            [FromQuery] int? v
        )
        {
            var video = await _context.MovieVideos
                .Include(mv => mv.Movie)
                .Include(mv => mv.Translation)
                .Include(mv => mv.Quality)
                .Include(mv => mv.Seasons
                    .OrderBy(ms => ms.SeasonNumber))
                .ThenInclude(ms => ms.Episodes
                    .OrderBy(me => me.SeriesNumber))
                .OrderByDescending(mv => mv.Seasons.Count)
                .FirstOrDefaultAsync(mv => v != null
                    ? mv.Id == v
                    : mv.Movie.Slug == movieSlug);


            if (video is null) return NotFound();

            return new GetMovieVideoDto()
            {
                Video = video,
                OtherMovieVideos = await _context.MovieVideos
                    .Include(mv => mv.Seasons
                        .OrderBy(ms => ms.SeasonNumber)
                        .Take(1))
                    .ThenInclude(ms => ms.Episodes
                        .OrderBy(me => me.SeriesNumber)
                        .Take(1))
                    .Include(mv => mv.Translation)
                    .Where(mv => mv.Movie.Slug == movieSlug)
                    .OrderByDescending(mv => mv.Seasons.Count)
                    .ToListAsync(),
            };
        }


        [HttpPost("AddLikeDislike/{movieId:int}")]
        [Authorize]
        public async Task<IActionResult> AddLikeDislikeToMovie(
            [FromRoute] int movieId,
            [FromBody] MovieLikeDislikeType type)
        {
            Guid.TryParse(User.Identity?.Name, out var guid);

            var user = await _context
                .Users
                .SingleOrDefaultAsync(u => u.Guid == guid);

            if (user is null) return Unauthorized();

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie is null)
                return BadRequest(new { Message = "Кино не найдено" });


            var likeFromDb = await _context
                .UserMovieLikeDislikes
                .SingleOrDefaultAsync(d => d.Movie == movie
                                           && d.User == user);

            if (likeFromDb is not null)
            {
                // recount with remove old like count
                switch (likeFromDb.Type)
                {
                    case MovieLikeDislikeType.Like:
                        movie.Likes -= 1;
                        break;
                    case MovieLikeDislikeType.Dislike:
                        movie.DisLikes -= 1;
                        break;
                }
            }

            if (likeFromDb is not null && likeFromDb.Type == type)
            {
                _context.Movies.Update(movie);
                _context.UserMovieLikeDislikes.Remove(likeFromDb);
                await _context.SaveChangesAsync();


                return Ok(new { movie.Likes, movie.DisLikes });
            }


            if (likeFromDb is null)
            {
                likeFromDb = new UserMovieLikeDislike
                {
                    Movie = movie,
                    User = user,
                };

                await _context.UserMovieLikeDislikes.AddAsync(likeFromDb);
            }


            // recount with new like
            switch (type)
            {
                case MovieLikeDislikeType.Like:
                    movie.Likes += 1;
                    break;
                case MovieLikeDislikeType.Dislike:
                    movie.DisLikes += 1;
                    break;
            }


            likeFromDb.Type = type;


            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();


            return Ok(new { movie.Likes, movie.DisLikes });
        }

        // PUT: api/Movies/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id) return BadRequest();

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'AppDbContext.Movies'  is null.");
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movies == null)
                return NotFound();

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}