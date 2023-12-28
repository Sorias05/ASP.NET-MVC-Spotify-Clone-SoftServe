using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPractice.Data;
using WebPractice.Data.Entities;

namespace WebPractice.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext context;

        public GenresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        private void LoadLists()
        {
            ViewBag.GenreList = new SelectList(context.Genres.ToList(), "Id", "Name");
            ViewBag.UserList = new SelectList(context.Users.ToList(), "Id", "Nickname");
            ViewBag.AlbumList = new SelectList(context.Albums.ToList(), "Id", "Name");
        }

        [HttpGet]
        public IActionResult Index()
        {
            LoadLists();
            List<Genre> genres = context.Genres.ToList();
            return View(genres);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadLists();
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                LoadLists();
                return View("Create");
            }

            context.Genres.Add(genre);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var genre = context.Genres.Find(id);
            if (genre == null) return NotFound();

            LoadLists();

            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                LoadLists();
                return View("Edit");
            }

            context.Genres.Update(genre);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = context.Genres.Find(id);

            if (item == null)
                return NotFound();

            context.Genres.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
