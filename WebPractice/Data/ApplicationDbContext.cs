using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPractice.Data.Entities;

namespace WebPractice.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Genre>().HasData(new[]
            {
                new Genre() { Id = 1, Name = "Classic" },
                new Genre() { Id = 2, Name = "Pop" },
                new Genre() { Id = 3, Name = "Rock" }
            });
            builder.Entity<SongPlaylist>().HasNoKey();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<SongPlaylist> SongPlaylists { get; set; }
    }
}