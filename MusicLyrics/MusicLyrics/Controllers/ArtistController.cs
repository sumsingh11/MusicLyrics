using Microsoft.AspNetCore.Mvc;
using MusicLyrics.Models;
using Microsoft.EntityFrameworkCore;
using MusicLyrics.Data;

namespace MusicLyrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtistController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all artists.
        /// </summary>
        /// <returns>A list of artist DTOs.</returns>
        /// <example>
        /// GET: api/Artists
        /// </example>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()
        {
            var artists = await _context.Artist
                .Select(a => new ArtistDTO
                {
                    ArtistId = a.ArtistId,
                    Name = a.Name,
                    Bio = a.Bio,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return Ok(artists);
        }

        /// <summary>
        /// Retrieves an artist by its ID.
        /// </summary>
        /// <param name="id">The artist ID.</param>
        /// <returns>The artist DTO if found; otherwise, NotFound.</returns>
        /// <example>
        /// GET: api/Artists/5
        /// </example>
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(int id)
        {
            var artist = await _context.Artist
                .Where(a => a.ArtistId == id)
                .Select(a => new ArtistDTO
                {
                    ArtistId = a.ArtistId,
                    Name = a.Name,
                    Bio = a.Bio,
                    CreatedAt = a.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        /// <summary>
        /// Creates a new artist.
        /// </summary>
        /// <param name="artistDTO">The artist data transfer object.</param>
        /// <returns>The created artist DTO.</returns>
        /// <example>
        /// POST: api/Artists
        /// Body: { "Name": "New Artist", "Bio": "Artist Bio", "CreatedAt": "2025-01-01T00:00:00Z" }
        /// </example>
        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> PostArtist(ArtistDTO artistDTO)
        {
            var artist = new Artist
            {
                Name = artistDTO.Name,
                Bio = artistDTO.Bio,
                CreatedAt = artistDTO.CreatedAt
            };

            _context.Artist.Add(artist);
            await _context.SaveChangesAsync();

            artistDTO.ArtistId = artist.ArtistId;

            return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistId }, artistDTO);
        }

        /// <summary>
        /// Updates an existing artist.
        /// </summary>
        /// <param name="id">The artist ID.</param>
        /// <param name="artistDTO">The updated artist DTO.</param>
        /// <returns>No content if successful; otherwise, BadRequest or NotFound.</returns>
        /// <example>
        /// PUT: api/Artists/5
        /// Body: { "ArtistId": 5, "Name": "Updated Artist", "Bio": "Updated Bio", "CreatedAt": "2025-02-01T00:00:00Z" }
        /// </example>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, ArtistDTO artistDTO)
        {
            if (id != artistDTO.ArtistId)
            {
                return BadRequest();
            }

            var artist = await _context.Artist.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            artist.Name = artistDTO.Name;
            artist.Bio = artistDTO.Bio;
            artist.CreatedAt = artistDTO.CreatedAt;

            _context.Entry(artist).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes an artist by its ID.
        /// </summary>
        /// <param name="id">The artist ID.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        /// <example>
        /// DELETE: api/Artists/5
        /// </example>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artist.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
