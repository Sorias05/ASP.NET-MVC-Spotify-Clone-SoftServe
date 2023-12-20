using WebPractice.Data.Entities;

namespace WebPractice.Models
{
    public class SongPlayerViewModel
    {
        public Song Song { get; set; }
        public bool IsCurrent { get; set; } = false;
        public bool IsInPlayer { get; set; } = false;
    }
}
