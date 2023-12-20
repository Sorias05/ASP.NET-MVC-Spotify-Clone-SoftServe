using System.ComponentModel.DataAnnotations;
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
        [NotMapped]
        public IFormFile? File { get; set; }
        //public string? UserId { get; set; }
        //public int FileId { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        //public User? User { get; set; }
        //public File? File { get; set; }
        public Album? Album { get; set; }
        public Genre? Genre { get; set; }

        [NotMapped]
        public ICollection<SongPlaylist>? SongPlaylists { get; set; }
    }
}
