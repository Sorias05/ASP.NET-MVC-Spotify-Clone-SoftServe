using WebPractice.Data.Entities;

namespace WebPractice.Models
{
    public class SongViewModel
    {
        public List<Song> Songs { get; set; }
        public int AlbumId { get; set; }
    }
}
