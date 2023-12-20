using NAudio.Wave;
using System.Globalization;
using WebPractice.Data;
using WebPractice.Data.Entities;
using WebPractice.Helpers;

namespace WebPractice.Services
{
    public interface IPlayerService
    {
        List<Song?> GetAll();
        Song currentSong { get; }
        WasapiOut wasapi { get; }
        void SetWasapi(WasapiOut wasapi);
        void SetCurrentSong(int id);
        void AddSong(int id);
        Song GetSong(int id);
    }

    public class PlayerService : IPlayerService
    {
        const string playerKey = "playerItems";
        private readonly HttpContext httpContext;
        private readonly ApplicationDbContext context;
        public Song currentSong { get; set; }
        public WasapiOut wasapi { get; set; }

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
        public void SetWasapi(WasapiOut wasapi)
        {
            this.wasapi = wasapi;
        }
        public void SetCurrentSong(int id)
        {
            currentSong = context.Songs.Find(id);
            //httpContext.Session.Set(playerKey, ids);
        }
        public Song GetSong(int id)
        {
            //var ids = httpContext.Session.Get<List<int>>(playerKey);
            //ids ??= new List<int>();
            
            return context.Songs.Find(id);
        }
    }
}
