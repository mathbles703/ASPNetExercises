using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using ASPNetExercises.Utils;
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
            HttpContext.Session.Remove(SessionVars.Tray); // clear out current tray
            HttpContext.Session.SetString(SessionVars.Message, "Tray Cleared"); 
            // clear out current cart once order has been placed
        return Redirect("/Home");
        }
    }
}