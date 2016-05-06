using ASPNetExercises.Models;
using System.Collections.Generic;
namespace ASPNetExercises.ViewModels
{
    public class MenuItemViewModel
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public int CAL { get; set; }
        public int CARB { get; set; }
        public string CATEGORY { get; set; }
        public int CHOL { get; set; }
        public decimal FAT { get; set; }
        public int FBR { get; set; }
        public int ITEM { get; set; }
        public int PRO { get; set; }
        public int SALT { get; set; }
        public decimal SFAT { get; set; }
        public int SGR { get; set; }
        public decimal TFAT { get; set; }
        public string JsonData { get; set; }
    }
}