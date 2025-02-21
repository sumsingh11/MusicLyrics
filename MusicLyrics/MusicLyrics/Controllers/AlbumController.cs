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

        // GET: api/Album
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

        // GET: api/Album/5
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

        // POST: api/Album
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

        // PUT: api/Album/5
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

        // DELETE: api/Album/5
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