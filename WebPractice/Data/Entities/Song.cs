﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPractice.Data.Entities
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        public string? FilePath { get; set; }
        public TimeSpan? Duration { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public Album? Album { get; set; }
        public Genre? Genre { get; set; }

        [NotMapped]
        public ICollection<SongPlaylist>? SongPlaylists { get; set; }
    }
}
