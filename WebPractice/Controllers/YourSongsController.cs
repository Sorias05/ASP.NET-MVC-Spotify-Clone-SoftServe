using Microsoft.AspNetCore.Mvc;

namespace WebPractice.Controllers
{
    public class YourSongsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
