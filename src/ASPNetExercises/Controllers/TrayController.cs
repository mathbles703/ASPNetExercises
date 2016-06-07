using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using ASPNetExercises.Models;
using ASPNetExercises.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ASPNetExercises.Controllers
{
    public class TrayController : Controller
    {
        AppDbContext _db;
        public TrayController(AppDbContext context)
        {
            _db = context;
        }
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

        // Add the tray, pass the session variable info to the db
        public ActionResult AddTray()
        {
            // they can't add a Tray if they're not logged on
            if (HttpContext.Session.GetString(SessionVars.User) == null)
            {
                return Redirect("/Login");
            }
            TrayModel model = new TrayModel(_db);
            int retVal = -1;
            string retMessage = "";
            try
            {
                Dictionary<string, object> trayItems =
                HttpContext.Session.GetObject<Dictionary<string, object>>(SessionVars.Tray);
                retVal = model.AddTray(trayItems,
                HttpContext.Session.GetString(SessionVars.User));
                if (retVal > 0) // Tray Added
                {
                    retMessage = "Tray " + retVal + " Created!";
                }
                else // problem
                {
                    retMessage = "Tray not added, try again later";
                }
            }
            catch (Exception ex) // big problem
            {
                retMessage = "Tray was not created, try again later! - " + ex.Message;
            }
            HttpContext.Session.Remove(SessionVars.Tray); // clear out current tray once persisted
            HttpContext.Session.SetString(SessionVars.Message, retMessage);
            return Redirect("/Home");
        }

        public IActionResult List()
        {
            if (HttpContext.Session.GetString(SessionVars.User) == null)
            {
                return Redirect("/Login");
            }
            return View("List");
        }

        [Route("[action]")]
        public IActionResult GetTrays()
        {
            TrayModel model = new TrayModel(_db);
            return Ok(model.GetTrays(HttpContext.Session.GetString(SessionVars.User)));
        }

        [Route("[action]/{tid:int}")]
        public IActionResult GetTrayDetails(int tid)
        {
            TrayModel model = new TrayModel(_db);
            return Ok(model.GetTrayDetails(tid, HttpContext.Session.GetString(SessionVars.User)));
        }

        [Route("[action]/{tid:int}")]
        public async Task<IActionResult> GetTrayDetailsAsync(int tid)
        {
            TrayModel model = new TrayModel(_db);
            List<ViewModels.TrayViewModel> details = await model.GetTrayDetailsAsync(tid,
            HttpContext.Session.GetString(SessionVars.User));
            return Ok(details);
        }
    }
}