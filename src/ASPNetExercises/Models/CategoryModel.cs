using System.Collections.Generic;
using System.Linq;

namespace ASPNetExercises.Models
{
    public class CategoryModel
    {
        private AppDbContext _db;
        public CategoryModel (AppDbContext ctx)
        {
            _db = ctx;
        }

        public List<Category> GetAll()
        {
            return _db.Categories.ToList<Category>();
        }
    }
}
