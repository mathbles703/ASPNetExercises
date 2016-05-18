using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
namespace ASPNetExercises.Controllers
{
    public class TrayController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ClearTray()
        {
            HttpContext.Session.Remove("tray"); // clear out current tray
            HttpContext.Session.SetString("Message", "Tray Cleared"); 
            // clear out current cart once order has been placed
        return Redirect("/Home");
        }
    }
}