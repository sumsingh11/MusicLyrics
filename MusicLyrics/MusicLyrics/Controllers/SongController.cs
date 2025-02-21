using Microsoft.AspNetCore.Mvc;
using MusicLyrics.Models;
using Microsoft.EntityFrameworkCore;
using MusicLyrics.Data;

namespace MusicLyrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SongController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all songs.
        /// </summary>
        /// <returns>A list of song DTOs.</returns>
        /// <example>
        /// GET: api/Songs
        /// </example>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs()
        {
            var songs = await _context.Songs
                .Include(s => s.Artist)  // Include Artist to fetch the artist details
                .Include(s => s.Album)   // Include Album to fetch the album details
                .Select(s => new SongDTO
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    ArtistId = s.ArtistId,
                    AlbumId = s.AlbumId,
                    Genre = s.Genre,
                    ReleaseDate = s.ReleaseDate
                })
                .ToListAsync();

            return Ok(songs);
        }

        /// <summary>
        /// Retrieves a song by its ID.
        /// </summary>
        /// <param name="id">The song ID.</param>
        /// <returns>The song DTO if found; otherwise, NotFound.</returns>
        /// <example>
        /// GET: api/Songs/5
        /// </example>
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> GetSong(int id)
        {
            var song = await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Where(s => s.SongId == id)
                .Select(s => new SongDTO
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    ArtistId = s.ArtistId,
                    AlbumId = s.AlbumId,
                    Genre = s.Genre,
                    ReleaseDate = s.ReleaseDate
                })
                .FirstOrDefaultAsync();

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        /// <summary>
        /// Creates a new song.
        /// </summary>
        /// <param name="songDTO">The song data transfer object.</param>
        /// <returns>The created song DTO.</returns>
        /// <example>
        /// POST: api/Songs
        /// Body: { "Title": "New Song", "ArtistId": 1, "AlbumId": 1, "Genre": "Rock", "ReleaseDate": "2025-01-01T00:00:00Z" }
        /// </example>
        [HttpPost]
        public async Task<ActionResult<SongDTO>> PostSong(SongDTO songDTO)
        {
            var song = new Song
            {
                Title = songDTO.Title,
                ArtistId = songDTO.ArtistId,
                AlbumId = songDTO.AlbumId,
                Genre = songDTO.Genre,
                ReleaseDate = songDTO.ReleaseDate
            };

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            songDTO.SongId = song.SongId;

            return CreatedAtAction(nameof(GetSong), new { id = song.SongId }, songDTO);
        }

        /// <summary>
        /// Updates an existing song.
        /// </summary>
        /// <param name="id">The song ID.</param>
        /// <param name="songDTO">The updated song DTO.</param>
        /// <returns>No content if successful; otherwise, BadRequest or NotFound.</returns>
        /// <example>
        /// PUT: api/Songs/5
        /// Body: { "SongId": 5, "Title": "Updated Song", "ArtistId": 2, "AlbumId": 2, "Genre": "Pop", "ReleaseDate": "2025-02-01T00:00:00Z" }
        /// </example>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, SongDTO songDTO)
        {
            if (id != songDTO.SongId)
            {
                return BadRequest();
            }

            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            song.Title = songDTO.Title;
            song.ArtistId = songDTO.ArtistId;
            song.AlbumId = songDTO.AlbumId;
            song.Genre = songDTO.Genre;
            song.ReleaseDate = songDTO.ReleaseDate;

            _context.Entry(song).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a song by its ID.
        /// </summary>
        /// <param name="id">The song ID.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        /// <example>
        /// DELETE: api/Songs/5
        /// </example>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
