using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetExercises.ViewModels
{
    public class TrayViewModel
    {
        public int TrayId { get; set; }
        public int MenuItemId { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string DateCreated { get; set; }
        public int TotalCalories { get; set; }
        public int TotalCholesterol { get; set; }
        public decimal TotalFat { get; set; }
        public int TotalFibre { get; set; }
        public int TotalProtein { get; set; }
        public int TotalSalt { get; set; }
    }
}