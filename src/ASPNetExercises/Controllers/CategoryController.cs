using Microsoft.AspNet.Mvc;
using ASPNetExercises.Models;
using ASPNetExercises.ViewModels;
using ASPNetExercises.Utils;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System;
namespace ASPNetExercises.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext _db;
        public CategoryController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            MenuViewModel vm = new MenuViewModel();
            // only build the catalogue once
            if (HttpContext.Session.GetObject<List<Category>>(SessionVars.Categories) == null)
            {
                try
                {
                    CategoryModel catModel = new CategoryModel(_db);
                    // now load the categories
                    List<Category> categories = catModel.GetAll();
                    HttpContext.Session.SetObject(SessionVars.Categories, categories);
                    vm.SetCategories(HttpContext.Session.GetObject<List<Category>>(SessionVars.Categories));
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            else
            {
                vm.SetCategories(HttpContext.Session.GetObject<List<Category>>(SessionVars.Categories));
            }
            return View(vm);
        }
        public IActionResult SelectCategory(MenuViewModel vm)
        {
            CategoryModel catModel = new CategoryModel(_db);
            MenuItemModel menuModel = new MenuItemModel(_db);
            List<MenuItem> items = menuModel.GetAllByCategory(vm.CategoryId);
            List<MenuItemViewModel> vms = new List<MenuItemViewModel>();
            if (items.Count > 0)
            {
                foreach (MenuItem item in items)
                {
                    MenuItemViewModel mvm = new MenuItemViewModel();
                    mvm.Qty = 0;
                    mvm.CategoryId = item.CategoryId;
                    mvm.CategoryName = catModel.GetName(item.CategoryId);
                    mvm.Description = item.Description;
                    mvm.Id = item.Id;
                    mvm.PRO = item.Protein;
                    mvm.SALT = item.Salt;
                    mvm.FAT = Convert.ToDecimal(item.Fat);
                    mvm.FBR = item.Fibre;
                    mvm.CHOL = item.Cholesterol;
                    mvm.CAL = item.Calories;
                    mvm.CARB = item.Carbs;
                    vms.Add(mvm);
                }
                MenuItemViewModel[] myMenu = vms.ToArray();
                HttpContext.Session.SetObject(SessionVars.Menu, myMenu);
            }
            vm.SetCategories(HttpContext.Session.GetObject<List<Category>>(SessionVars.Categories));
            return View("Index", vm);
        }
        [HttpPost]
        public ActionResult SelectItem(MenuViewModel vm)
        {
            Dictionary<int, object> tray;
            if (HttpContext.Session.GetObject<Dictionary<int, Object>>(SessionVars.Tray) == null)   //int or string?
            {
                tray = new Dictionary<int, object>();
            }
            else
            {
                tray = HttpContext.Session.GetObject<Dictionary<int, object>>(SessionVars.Tray);
            }
            MenuItemViewModel[] menu =
            HttpContext.Session.GetObject<MenuItemViewModel[]>(SessionVars.Menu);
            String retMsg = "";
            foreach (MenuItemViewModel item in menu)
            {
                if (item.Id == vm.Id)
                {
                    if (vm.Qty > 0) // update only selected item
                    {
                        item.Qty = vm.Qty;
                        retMsg = vm.Qty + " - item(s) Added!";
                        tray[item.Id] = item;
                    }
                    else
                    {
                        item.Qty = 0;
                        tray.Remove(item.Id);
                        retMsg = "item(s) Removed!";
                    }
                    vm.CategoryId = item.CategoryId;
                    break;
                }
            }
            ViewBag.AddMessage = retMsg;
            HttpContext.Session.SetObject(SessionVars.Tray, tray);
            vm.SetCategories(HttpContext.Session.GetObject<List<Category>>(SessionVars.Categories));
            return View("Index", vm);
        }
    }
}