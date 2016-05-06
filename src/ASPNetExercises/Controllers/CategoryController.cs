using ASPNetExercises.Models;
using ASPNetExercises.ViewModels;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNetExercises.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext _db;
        public CategoryController (AppDbContext context)
        {
            _db = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            CategoryModel model = new CategoryModel(_db);
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.Categories = model.GetAll();
            return View(viewModel);
        }
    }
}
