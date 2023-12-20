using Microsoft.AspNetCore.Identity;

namespace WebPractice.Data.Entities
{
    public class User : IdentityUser
    {
        public string Nickname { get; set; }
        public ICollection<Album>? Albums { get; set; }
        public ICollection<Song>? Songs { get; set; }
        public ICollection<Playlist>? Playlists { get; set; }
    }
}
