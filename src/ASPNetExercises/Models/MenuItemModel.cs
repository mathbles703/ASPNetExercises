using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ASPNetExercises.Models
{
    public class MenuItemModel
    {
        /// <summary>
        ///  MenuItemModel - Model class representing a MenuItem
        ///     Author:     Evan Lauersen
        ///     Date:       Created: Feb 27, 2016
        ///     Purpose:    Model class to interface with DB and feed data to 
        ///                 Controller
        /// </summary>
        private AppDbContext _db;
        /// <summary>
        /// constructor should pass instantiated DbContext
        /// <summary>
        public MenuItemModel(AppDbContext context)
        {
            _db = context;
        }
        public bool loadCategories(string rawJson)
        {
            bool loadedCategories = false;
            try
            {
                // clear out the old rows
                _db.Categories.RemoveRange(_db.Categories);
                _db.SaveChanges();

                dynamic decodedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(rawJson);
                List<String> allCategories = new List<String>();

                foreach (var c in decodedJson)
                {
                    allCategories.Add(Convert.ToString(c["CATEGORY"]));  
                }

                // distinct will remove duplicates before we insert them into the db
                IEnumerable<String>categories = allCategories.Distinct<String>();

                foreach (string c in categories)
                {
                    Category cat = new Category();
                    cat.Name = c;
                    _db.Categories.Add(cat);
                    _db.SaveChanges();
                }
                loadedCategories = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedCategories;
        }
        public bool loadMenuItems(string rawJson)
        {
            bool loadedItems = false;
            try
            {
                List<Category> categories = _db.Categories.ToList();
                // clear out the old
                _db.MenuItems.RemoveRange(_db.MenuItems);
                _db.SaveChanges();
                string decodedJsonStr = Decoder(rawJson);
                dynamic menuItemJson = Newtonsoft.Json.JsonConvert.DeserializeObject(decodedJsonStr);
                foreach (var m in menuItemJson)
                {
                    MenuItem item = new MenuItem();
                    item.Calories = Convert.ToInt32(m["CAL"]);
                    item.Carbs = Convert.ToInt32(m["CARB"]);
                    item.Cholesterol = Convert.ToInt32(m["CHOL"]);
                    item.Fat = Convert.ToSingle(m["FAT"]);
                    item.Fibre = Convert.ToInt32(m["FBR"]);
                    item.Protein = Convert.ToInt32(m["PRO"]);
                    item.Salt = Convert.ToInt32(m["SALT"]);
                    string cat = Convert.ToString(m["CATEGORY"]);

                    foreach (Category category in categories)
                    {
                        if (category.Name == cat)
                            item.CategoryId = category.Id;
                    }

                    // decoder stripped out the 'n'
                    if (Convert.ToString(m["ITEM"]) == "Jalapeo")
                    {
                        item.Description = "Jalapeno";
                    }
                    else
                        item.Description = Convert.ToString(m["ITEM"]);

                    _db.MenuItems.Add(item);
                    _db.SaveChanges();
                }
                loadedItems = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedItems;
        }
        public string Decoder(string value)
        {
            Regex regex = new Regex(@"\\u(?<Value>[a-zA-Z0-9]{4})", RegexOptions.Compiled);
            return regex.Replace(value, "");
        }
        public List<MenuItem> GetAll()
        {
            return _db.MenuItems.ToList();
        }
        public List<MenuItem> GetAllByCategory(int id)
        {
            return _db.MenuItems.Where(item => item.CategoryId == id).ToList();
        }

        public List<MenuItem> GetAllByCategoryName(string catname)
        {
            Category category = _db.Categories.First(cat => cat.Name == catname);
            return _db.MenuItems.Where(item => item.CategoryId == category.Id).ToList();
        }
    }
}
