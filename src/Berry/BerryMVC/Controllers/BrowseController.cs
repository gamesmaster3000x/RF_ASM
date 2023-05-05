using BerryMVC.Data;
using BerryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BerryMVC.Controllers
{
    public class BrowseController : Controller
    {
        private readonly ILogger<HomeController> LOGGER;
        private readonly BerryMVCContext _berryMVCContext;

        public BrowseController (ILogger<HomeController> logger, BerryMVCContext berryMVCContext)
        {
            LOGGER = logger;
            _berryMVCContext = berryMVCContext;
        }

        // GET: BerryModels
        public IActionResult Index ()
        {
            return View(GetTrending());
        }

        public IActionResult ViewBerry (string name)
        {
            if (String.IsNullOrWhiteSpace(name)) RedirectToAction("Index");
            BerryModel? model = GetBerry(name);
            return model != null ? View(model) : NotFound($"The Berry '{name}' was not found.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error ()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public BerryModel? GetBerry (string name)
        {
            if (_berryMVCContext?.BerryModel == null) return null;
            IQueryable<BerryModel> queryResult = from berry in _berryMVCContext.BerryModel
                                                 select berry;
            IList<BerryModel> list = queryResult.ToList();
            IEnumerable<BerryModel> result = list
                .Where(b => b.FullName.Equals(name)
                );
            if (result == null || result.ToList().Count == 0) return null;
            else return result.FirstOrDefault();
        }

        public IEnumerable<BerryModel> GetTrending ()
        {
            if (_berryMVCContext.BerryModel == null) return Enumerable.Empty<BerryModel>();

            return (from berry in _berryMVCContext.BerryModel
                    where berry != null
                    orderby berry.ArtifactName
                    select berry)
                .Take(5)
                ;

        }
    }
}