using BerryMVC.Data;
using BerryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BerryMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> LOGGER;
        private readonly BerryMVCContext _berryMVCContext;

        public HomeController (ILogger<HomeController> logger, BerryMVCContext berryMVCContext)
        {
            LOGGER = logger;
            _berryMVCContext = berryMVCContext;
        }

        // GET: BerryModels
        public IActionResult Index ()
        {
            return View();
        }

        public IActionResult Privacy ()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error ()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}