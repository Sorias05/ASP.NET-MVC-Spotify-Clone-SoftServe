using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPractice.Models;
using WebPractice.Data;
using WebPractice.Data.Entities;
using WebPractice.Services;
using WebPractice.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NAudio.Wave;
using System;
using WebPractice.Helpers;


namespace WebPractice.Controllers
{
    //[Authorize(Roles = "User")]
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

        //public IActionResult Play(int id)
        //{
        //    playerService.AddSong(id);
        //    var song = context.Songs.Find(id);
        //    string path = "./wwwroot/MP3Files/" + song.FilePath;
        //    using (var mf = new MediaFoundationReader(path))
        //    using (var wo = new WasapiOut())
        //    {
        //        playerService.SetWasapi(wo);
        //        wo.Volume = 0.5f;
        //        wo.Init(mf);
        //        wo.Play();
        //        while (wo.PlaybackState == PlaybackState.Playing)
        //        {
        //            Thread.Sleep(1000);
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        //public IActionResult Continue(WasapiOut wo)
        //{
        //    playerService.SetWasapi(wo);
        //    wo.Play();
        //    return RedirectToAction("Index");
        //}

        //public IActionResult Pause(WasapiOut wo)
        //{
        //    playerService.SetWasapi(wo);
        //    wo.Pause();
        //    return RedirectToAction("Index");
        //}

        //public IActionResult Stop(WasapiOut wo)
        //{
        //    playerService.SetWasapi(wo);
        //    wo.Stop();
        //    return RedirectToAction("Index");
        //}

        public IActionResult Player(int id)
        {
            playerService.AddSong(id);
            playerService.SetCurrentSong(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}