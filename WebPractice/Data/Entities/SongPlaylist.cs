using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebPractice.Data.Entities
{
    public class SongPlaylist
    {
        public int SongId { get; set; }
        public Song? Song { get; set; }

        public int PlaylistId { get; set; }
        public Playlist? Playlist { get; set; }
    }
}
