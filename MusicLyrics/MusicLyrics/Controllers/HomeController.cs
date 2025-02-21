using Microsoft.AspNetCore.Mvc;
using MusicLyrics.Models;
using System.Diagnostics;

namespace MusicLyrics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Serves the home page.
        /// </summary>
        /// <returns>The Index view.</returns>
        /// <example>
        /// GET: /
        /// </example>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Serves the privacy policy page.
        /// </summary>
        /// <returns>The Privacy view.</returns>
        /// <example>
        /// GET: /Home/Privacy
        /// </example>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Handles error pages and logs error details.
        /// </summary>
        /// <returns>The Error view with error details.</returns>
        /// <example>
        /// GET: /Home/Error
        /// </example>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
