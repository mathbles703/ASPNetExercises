using Microsoft.AspNet.Mvc;
using ASPNetExercises.Models;
using ASPNetExercises.Utils;
using Microsoft.AspNet.Http;
namespace ASPNetExercises.Controllers
{
    public class StoreController : Controller
    {
        AppDbContext _db;
        public StoreController(AppDbContext context)
        {
            _db = context;
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionVars.Message) != null)
            {
                ViewBag.Message = HttpContext.Session.GetString(SessionVars.Message);
            }
            return View();
        }
        [Route("[action]/{lat:double}/{lng:double}")]
        public IActionResult GetStores(float lat, float lng)
        {
            StoreModel model = new StoreModel(_db);
            return Ok(model.GetThreeClosetStores(lat, lng));
        }
    }
}