using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebPractice.Data.Entities;
using WebPractice.Data;
using Microsoft.EntityFrameworkCore;

namespace WebPractice.Controllers
{
    public class YourAlbumsController : Controller
    {
        private readonly ApplicationDbContext context;
        public YourAlbumsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        private void LoadLists()
        {
            ViewBag.AlbumList = new SelectList(context.Albums.ToList(), "Id", "Name");
            ViewBag.UserList = new SelectList(context.Users.ToList(), "Id", "Nickname");
        }

        [HttpGet]
        public IActionResult Index()
        {
            LoadLists();
            List<Album> albums = context.Albums.Include(x => x.User).ToList();
            return View(albums);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadLists();
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Album album)
        {
            album.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!ModelState.IsValid)
            {
                LoadLists();
                return View("Create");
            }

            context.Albums.Add(album);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var album = context.Albums.Find(id);
            if (album == null) return NotFound();

            LoadLists();

            return View(album);
        }

        [HttpPost]
        public IActionResult Edit(Album album)
        {
            album.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!ModelState.IsValid)
            {
                LoadLists();
                return View("Edit");
            }

            context.Albums.Update(album);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = context.Albums.Find(id);

            if (item == null)
                return NotFound();

            context.Albums.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
