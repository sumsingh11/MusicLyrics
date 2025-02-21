using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLyrics.Models
{
    /// <summary>
    /// Represents a song with details such as title, artist, album, genre, and release date.
    /// </summary>
    public class Song
    {
        /// <summary>
        /// Gets or sets the unique identifier for the song.
        /// </summary>
        [Key]
        public int SongId { get; set; }

        /// <summary>
        /// Gets or sets the title of the song.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the ID of the artist associated with the song.
        /// </summary>
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Navigation property for the artist of the song.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the ID of the album associated with the song (nullable).
        /// </summary>
        [ForeignKey("Album")]
        public int? AlbumId { get; set; }

        /// <summary>
        /// Navigation property for the album of the song.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the genre of the song.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the release date of the song (nullable).
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for a song.
    /// </summary>
    public class SongDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the song.
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// Gets or sets the title of the song.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the ID of the artist associated with the song.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the album associated with the song (nullable).
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the genre of the song.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the release date of the song (nullable).
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
    }
}
