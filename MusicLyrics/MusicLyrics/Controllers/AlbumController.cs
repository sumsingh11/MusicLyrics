using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLyrics.Data;
using MusicLyrics.Models;

namespace MusicLyrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlbumController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all albums.
        /// </summary>
        /// <returns>A list of album DTOs.</returns>
        /// <example>
        /// GET: api/Album
        /// </example>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbums()
        {
            return await _context.Albums
                .Select(a => new AlbumDTO
                {
                    AlbumId = a.AlbumId,
                    Title = a.Title,
                    ReleaseDate = a.ReleaseDate,
                    CoverImage = a.CoverImage,
                    ArtistId = a.ArtistId
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves an album by its ID.
        /// </summary>
        /// <param name="id">The album ID.</param>
        /// <returns>The album DTO if found; otherwise, NotFound.</returns>
        /// <example>
        /// GET: api/Album/5
        /// </example>
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDTO>> GetAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return new AlbumDTO
            {
                AlbumId = album.AlbumId,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                CoverImage = album.CoverImage,
                ArtistId = album.ArtistId
            };
        }

        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <param name="albumDto">The album data transfer object.</param>
        /// <returns>The created album DTO.</returns>
        /// <example>
        /// POST: api/Album
        /// Body: { "Title": "New Album", "ReleaseDate": "2025-01-01", "CoverImage": "url.jpg", "ArtistId": 1 }
        /// </example>
        [HttpPost]
        public async Task<ActionResult<AlbumDTO>> PostAlbum(AlbumDTO albumDto)
        {
            var album = new Album
            {
                Title = albumDto.Title,
                ReleaseDate = albumDto.ReleaseDate,
                CoverImage = albumDto.CoverImage,
                ArtistId = albumDto.ArtistId
            };

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlbum), new { id = album.AlbumId }, albumDto);
        }

        /// <summary>
        /// Updates an existing album.
        /// </summary>
        /// <param name="id">The album ID.</param>
        /// <param name="albumDto">The updated album DTO.</param>
        /// <returns>No content if successful; otherwise, BadRequest or NotFound.</returns>
        /// <example>
        /// PUT: api/Album/5
        /// Body: { "AlbumId": 5, "Title": "Updated Album", "ReleaseDate": "2025-02-01", "CoverImage": "new_url.jpg", "ArtistId": 1 }
        /// </example>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, AlbumDTO albumDto)
        {
            if (id != albumDto.AlbumId)
            {
                return BadRequest();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            album.Title = albumDto.Title;
            album.ReleaseDate = albumDto.ReleaseDate;
            album.CoverImage = albumDto.CoverImage;
            album.ArtistId = albumDto.ArtistId;

            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes an album by its ID.
        /// </summary>
        /// <param name="id">The album ID.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        /// <example>
        /// DELETE: api/Album/5
        /// </example>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
