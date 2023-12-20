using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebPractice.Data.Entities;

namespace WebPractice.Data.DTO
{
    public class SongDTO
    {
        public string Name { get; set; }
        public string? FilePath { get; set; }
        public IFormFile? File { get; set; }
        //public string? UserId { get; set; }
        //public int FileId { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        //public User? User { get; set; }
        //public File? File { get; set; }
        public Album? Album { get; set; }
        public Genre? Genre { get; set; }
        public ICollection<SongPlaylist>? SongPlaylists { get; set; }
    }
}
