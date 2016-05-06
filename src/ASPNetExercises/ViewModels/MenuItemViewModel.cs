using ASPNetExercises.Models;
using System.Collections.Generic;
namespace ASPNetExercises.ViewModels
{
    public class MenuItemViewModel
    {
        public string CategoryName { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}