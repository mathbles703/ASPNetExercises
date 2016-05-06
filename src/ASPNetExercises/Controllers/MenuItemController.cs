using ASPNetExercises.Models;
using ASPNetExercises.ViewModels;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNetExercises.Controllers
{
    public class MenuItemController : Controller
    {
        AppDbContext _db;
        public MenuItemController(AppDbContext context)
        {
            _db = context;
        }
        // GET: /<controller>/
        public IActionResult Index(CategoryViewModel category)
        {
            MenuItemModel model = new MenuItemModel(_db);
            MenuItemViewModel viewModel = new MenuItemViewModel();
            viewModel.CategoryName = category.CategoryName;
            viewModel.MenuItems = model.GetAllByCategoryName(category.CategoryName);
            return View(viewModel);
        }
    }
}
