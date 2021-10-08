using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using serverside_aspnet.Models;

namespace serverside_aspnet.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private ILogger<ControllerBase> _logger;

        protected ControllerBase(ILogger<ControllerBase> logger)
        {
            _logger = logger;
        }

        public virtual IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public virtual IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
