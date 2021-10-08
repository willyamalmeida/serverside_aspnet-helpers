

using Microsoft.AspNetCore.Mvc;

namespace serverside_aspnet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
