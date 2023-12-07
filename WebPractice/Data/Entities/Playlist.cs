using System.ComponentModel.DataAnnotations.Schema;

namespace WebPractice.Data.Entities
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public ICollection<SongPlaylist> SongPlaylists { get; set; }
    }
}
