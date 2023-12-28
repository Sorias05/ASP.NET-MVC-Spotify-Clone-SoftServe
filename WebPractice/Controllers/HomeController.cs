using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPractice.Models;
using WebPractice.Data;
using WebPractice.Data.Entities;
using WebPractice.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPractice.Helpers;

namespace WebPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IPlayerService playerService;

        public HomeController(ApplicationDbContext context, IPlayerService playerService)
        {
            this.context = context;
            this.playerService = playerService;
        }

        private void LoadLists()
        {
            ViewBag.GenreList = new SelectList(context.Genres.ToList(), "Id", "Name");
            ViewBag.UserList = new SelectList(context.Users.ToList(), "Id", "Nickname");
            ViewBag.AlbumList = new SelectList(context.Albums.ToList(), "Id", "Name");
        }

        private bool IsSongInPlayer(int id)
        {
            List<int> ids = HttpContext.Session.Get<List<int>>("playerItems");
            if (ids == null) return false;
            return ids.Contains(id);
        }

        public IActionResult Index()
        {
            List<Song> songs = context.Songs.Include(x => x.Genre).ToList();
            var songsPlayerViewModel = songs.Select(
                s => new SongPlayerViewModel
                    {
                        Song = s,
                        IsInPlayer = IsSongInPlayer(s.Id),
                    }
                ).ToList();
            
            
            LoadLists();
            return View(songsPlayerViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Player(int id)
        {
            playerService.AddSong(id);
            playerService.SetCurrentSong();
            return RedirectToAction("Index");
        }

        public IActionResult AddToOrder(int id)
        {
            playerService.AddSong(id);
            return RedirectToAction("Index");
        }

        public IActionResult Prev(int id)
        {
            var order = HttpContext.Session.Get<List<int>>("playerItems");
            var counter = HttpContext.Session.Get<int>("counter");
            if (order != null && counter != null)
            {
                if (counter <= 1)
                {
                    counter = order.Count;
                }
                else
                {
                    counter--;
                }
            }
            HttpContext.Session.Set("counter", counter);
            return RedirectToAction("Index");
        }

        public IActionResult Next(int id)
        {
            var order = HttpContext.Session.Get<List<int>>("playerItems");
            var counter = HttpContext.Session.Get<int>("counter");
            if(order != null && counter != null) 
            {
                if (counter >= order.Count)
                {
                    counter = 1;
                }
                else
                {
                    counter++;
                }
            }
            HttpContext.Session.Set("counter", counter);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}