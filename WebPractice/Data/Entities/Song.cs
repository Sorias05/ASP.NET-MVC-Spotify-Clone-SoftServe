using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPractice.Data.Entities
{
    public class Song
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public string? UserId { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        [NotMapped]
        public ICollection<SongPlaylist> SongPlaylists { get; set; }
    }
}
