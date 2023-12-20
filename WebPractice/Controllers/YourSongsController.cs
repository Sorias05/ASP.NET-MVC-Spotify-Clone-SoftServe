using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using WebPractice.Data;
using WebPractice.Data.Entities;
using WebPractice.Models;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using WebPractice.Data.DTO;

namespace WebPractice.Controllers
{
    public class YourSongsController : Controller
    {
        private readonly ApplicationDbContext context;
        private IHostingEnvironment Environment;
        private SongViewModel songViewModel = new SongViewModel();

        public YourSongsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.Environment = hostingEnvironment;
        }

        private void LoadLists()
        {
            ViewBag.GenreList = new SelectList(context.Genres.ToList(), "Id", "Name");
            ViewBag.UserList = new SelectList(context.Users.ToList(), "Id", "Nickname");
            ViewBag.AlbumList = new SelectList(context.Albums.ToList(), "Id", "Name");
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            List<Song> songs = context.Songs.Include(x => x.Genre).ToList();
            songViewModel.Songs = songs;
            songViewModel.AlbumId = id;
            LoadLists();
            
            return View(songViewModel);
        }

        [HttpGet]
        public IActionResult Add(int albumId)
        {
            SongDTO songDto = new SongDTO();
            songDto.AlbumId = albumId;
            LoadLists();

            return View(songDto);
        }

        [HttpPost]
        public IActionResult Add(SongDTO songDto)
        {
            if (songDto.File != null) songDto.FilePath = UploadFile(songDto.File);

            if (!ModelState.IsValid || songDto.FilePath == null)
            {
                LoadLists();
                return View("Add");
            }

            Song song = new Song() { 
                Name = songDto.Name,
                FilePath = songDto.FilePath,
                AlbumId = songDto.AlbumId,
                GenreId = songDto.GenreId
            };
            
            context.Songs.Add(song);
            context.SaveChanges();

            Console.WriteLine("Song added");

            return RedirectToAction("Index", new { id = song.AlbumId });
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var song = context.Songs.Find(id);
            if (song == null) return NotFound();

            string path = "./wwwroot/MP3Files/" + song.FilePath;
            using (var stream = System.IO.File.OpenRead(path)) 
                song.File = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            LoadLists();

            return View(song);
        }

        [HttpPost]
        public IActionResult Edit(Song song)
        {
            if (song.File != null) song.FilePath = UploadFile(song.File);

            if (!ModelState.IsValid || song.FilePath == null)
            {
                LoadLists();
                return View("Edit");
            }

            context.Songs.Update(song);
            context.SaveChanges();

            return RedirectToAction("Index", new { id = song.AlbumId });
        }

        public IActionResult Delete(int id)
        {
            var item = context.Songs.Find(id);
            
            if (item == null)
                return NotFound();

            int aId = item.AlbumId;
            context.Songs.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", new { id = aId });
        }

        private string UploadFile(IFormFile postedFile)
        {
            string path = Path.Combine(this.Environment.WebRootPath, "MP3Files");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("yyyy''MM''dd'-'HH''mm''ss") + Path.GetExtension(postedFile.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            //songViewModel.filePath = Path.Combine(path, fileName);
            //Console.WriteLine(songViewModel.filePath);
            //ViewBag.Data = "data:audio/wav;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(path, fileName)));
            return fileName;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
