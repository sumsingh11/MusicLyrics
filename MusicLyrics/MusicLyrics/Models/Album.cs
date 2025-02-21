using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MusicLyrics.Models
{
    /// <summary>
    /// Represents an album in the music library.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Gets or sets the unique identifier for the album.
        /// </summary>
        [Key]
        public int AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the title of the album.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the release date of the album.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the cover image URL of the album.
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated artist.
        /// </summary>
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Navigation property for the associated artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Navigation property for the collection of songs in the album.
        /// </summary>
        public ICollection<Song> Songs { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for an album.
    /// </summary>
    public class AlbumDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the album.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the title of the album.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the release date of the album.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the cover image URL of the album.
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated artist.
        /// </summary>
        public int ArtistId { get; set; }
    }
}
