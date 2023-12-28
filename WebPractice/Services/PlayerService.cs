using WebPractice.Data;
using WebPractice.Data.Entities;
using WebPractice.Helpers;

namespace WebPractice.Services
{
    public interface IPlayerService
    {
        List<Song?> GetAll();
        Song currentSong { get; }
        void SetCurrentSong();
        int GetCounter();
        void AddSong(int id);
        Song GetSong(int id);
    }

    public class PlayerService : IPlayerService
    {
        const string playerKey = "playerItems";
        const string playerCounter = "counter";
        private readonly HttpContext httpContext;
        private readonly ApplicationDbContext context;
        public Song currentSong { get; set; }

        public PlayerService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.context = context;
        }
        
        public List<Song?> GetAll()
        {
            var ids = httpContext.Session.Get<List<int>>(playerKey);
            ids ??= new List<int>();

            return ids.Select(id => context.Songs.Find(id)).ToList();
        }
        public void AddSong(int id)
        {
            var ids = httpContext.Session.Get<List<int>>(playerKey);
            ids ??= new List<int>();

            ids.Add(id);
            httpContext.Session.Set(playerKey, ids);
        }
        public void SetCurrentSong()
        {
            //currentSong = context.Songs.Find(id);
            var ids = httpContext.Session.Get<List<int>>(playerKey);
            ids ??= new List<int>();
            var counter = httpContext.Session.Get<int>(playerCounter);
            if (counter == null)
                counter = 0;

            counter = ids.Count;
            httpContext.Session.Set(playerCounter, counter);
        }
        public int GetCounter()
        {
            var counter = httpContext.Session.Get<int>(playerCounter);
            if (counter == null)
                counter = 0;

            return counter;
        }
        public Song GetSong(int id)
        {
            //var ids = httpContext.Session.Get<List<int>>(playerKey);
            //ids ??= new List<int>();
            
            return context.Songs.Find(id);
        }
    }
}
