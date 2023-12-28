using WebPractice.Data.Entities;

namespace WebPractice.Data.DTO
{
    public class SongDTO
    {
        public string Name { get; set; }
        public string? FilePath { get; set; }
        public IFormFile? File { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public Album? Album { get; set; }
        public Genre? Genre { get; set; }
        public ICollection<SongPlaylist>? SongPlaylists { get; set; }
    }
}
