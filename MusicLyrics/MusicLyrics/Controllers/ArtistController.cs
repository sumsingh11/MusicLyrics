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

        // GET: api/Artists
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

        // GET: api/Artists/5
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

        // POST: api/Artists
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

        // PUT: api/Artists/5
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

        // DELETE: api/Artists/5
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
