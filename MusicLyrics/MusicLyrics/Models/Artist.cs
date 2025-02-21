using System.ComponentModel.DataAnnotations;
namespace MusicLyrics.Models
{
    /// <summary>
    /// Represents a music artist with associated albums and songs.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Gets or sets the unique identifier for the artist.
        /// </summary>
        [Key]
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the biography of the artist.
        /// </summary>
        public required string Bio { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the artist was created in the system.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Navigation property for the albums associated with the artist.
        /// </summary>
        public ICollection<Album> Albums { get; set; }

        /// <summary>
        /// Navigation property for the songs associated with the artist.
        /// </summary>
        public ICollection<Song> Songs { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for an artist.
    /// </summary>
    public class ArtistDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the artist.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the biography of the artist.
        /// </summary>
        public required string Bio { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the artist was created in the system.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
