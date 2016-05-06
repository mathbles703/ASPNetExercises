using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using ASPNetExercises.Models;
namespace ASPNetExercises.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel() { }
        public string CategoryName { get; set; }
        public List<Category> Categories { get; set; }
        public IEnumerable<SelectListItem> GetCategoryNames()
        {
            return Categories.Select(category => new SelectListItem
            {
                Text = category.Name,
                Value
            = category.Name
            });
        }
    }
}