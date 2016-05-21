using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using ASPNetExercises.ViewModels;
namespace ASPNetExercises.Models
{
    public class TrayModel
    {
        private AppDbContext _db;
        public TrayModel(AppDbContext ctx)
        {
            _db = ctx;
        }
        public int AddTray(Dictionary<string, object> items, string user)
        {
            int trayId = -1;
            using (_db)
            {
                // we need a transaction as multiple entities involved
                using (var _trans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        Tray tray = new Tray();
                        tray.UserId = user;
                        tray.DateCreated = System.DateTime.Now;
                        tray.TotalCalories = 0;
                        tray.TotalCholesterol = 0;
                        tray.TotalFat = 0;
                        tray.TotalFibre = 0;
                        tray.TotalSalt = 0;
                        tray.TotalProtein = 0;
                        // calculate the totals and then add the tray row to the table
                        foreach (var key in items.Keys)
                        {
                            MenuItemViewModel item =
                            JsonConvert.DeserializeObject<MenuItemViewModel>(Convert.ToString(items[key]));
                            if (item.Qty > 0)
                            {
                                tray.TotalCalories += item.CAL * item.Qty;
                                tray.TotalCholesterol += item.CHOL * item.Qty;
                                tray.TotalFat += item.FAT * item.Qty;
                                tray.TotalFibre += item.FBR * item.Qty;
                                tray.TotalSalt += item.SALT * item.Qty;
                                tray.TotalProtein += item.PRO * item.Qty;
                            }
                        }
                        _db.Trays.Add(tray);
                        _db.SaveChanges();
                        // then add each item to the trayitems table
                        foreach (var key in items.Keys)
                        {
                            MenuItemViewModel item =
                            JsonConvert.DeserializeObject<MenuItemViewModel>(Convert.ToString(items[key]));
                            if (item.Qty > 0)
                            {
                                TrayItem tItem = new TrayItem();
                                tItem.Qty = item.Qty;
                                tItem.MenuItemId = item.Id;
                                tItem.TrayId = tray.Id;
                                _db.TrayItems.Add(tItem);
                                _db.SaveChanges();
                            }
                        }
                        // test trans by uncommenting out these 3 lines
                        //int x = 1;
                        //int y = 0;
                        //x = x / y;
                        _trans.Commit();
                        trayId = tray.Id;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _trans.Rollback();
                    }
                }
            }
            return trayId;
        }
    }
}