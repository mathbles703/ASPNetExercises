using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNetExercises.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Message = HttpContext.Session.GetString("Message");
            return View();
        }
    }
}
