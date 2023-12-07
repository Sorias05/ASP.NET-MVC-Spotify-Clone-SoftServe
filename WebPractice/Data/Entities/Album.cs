namespace WebPractice.Data.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserId { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
