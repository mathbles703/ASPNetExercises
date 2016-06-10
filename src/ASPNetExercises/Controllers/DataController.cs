using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNetExercises.Models;
using System.Net.Http;
using Microsoft.AspNet.Hosting;

namespace ASPNetExercises.Controllers
{
    public class DataController : Controller
    {
        AppDbContext _db;
        IHostingEnvironment _env;
        public DataController(AppDbContext context, IHostingEnvironment env)
        {
            _db = context;
            _env = env;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Json()
        {
            MenuItemModel model = new MenuItemModel(_db);
            string rawJsonString = await getMenuItemJsonFromWeb();
            bool categoriesLoaded = model.loadCategories(rawJsonString);
            bool menuItemsLoaded = model.loadMenuItems(rawJsonString);
            if (categoriesLoaded && menuItemsLoaded)
                ViewBag.JsonLoadedMsg = "Json Loaded Successfully";
            else
                ViewBag.JsonLoadedMsg = "Json NOT Loaded";
            return View("Index");
        }
        private async Task<String> getMenuItemJsonFromWeb()
        {
            string url = "https://raw.githubusercontent.com/pffy/data-mcdonalds-nutritionfacts/master/json/mcd.json";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public IActionResult Csv()
        {
            StoreModel model = new StoreModel(_db);
            bool storesLoaded = model.LoadFromFile(_env.WebRootPath);
            if (storesLoaded)
                ViewBag.CsvLoadedMsg = "Csv Loaded Successfully";
            else
                ViewBag.CsvLoadedMsg = "Csv NOT Loaded";
            return View("Index");
        }
    }
}
