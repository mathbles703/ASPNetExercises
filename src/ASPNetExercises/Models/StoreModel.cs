using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ASPNetExercises.Models
{
    public class StoreModel
    {
        private AppDbContext _db;
        public StoreModel(AppDbContext context)
        {
            _db = context;
        }
        public bool LoadFromFile(string path)
        {
            bool addWorked = false;
            try
            {
                // clear out the old rows
                _db.Stores.RemoveRange(_db.Stores);
                _db.SaveChanges();
                var csv = new List<string[]>();
                var csvFile = path + "\\storeRaw.csv";
                var lines = System.IO.File.ReadAllLines(csvFile);
                foreach (string line in lines)
                    csv.Add(line.Split(',')); // populate store with csv
                foreach (string[] x in csv)
                {
                    Store aStore = new Store();
                    aStore.Longitude = Convert.ToDouble(x[0]);
                    aStore.Latitude = Convert.ToDouble(x[1]);
                    aStore.Street = x[2];
                    aStore.City = x[3];
                    aStore.Region = x[4];
                    _db.Stores.Add(aStore);
                    _db.SaveChanges();
                }
                addWorked = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return addWorked;
        }

        public List<Store> GetThreeClosetStores(float? lat, float? lng)
        {
            List<Store> storeDetails = null;
            try
            {
                var latParam = new SqlParameter("@lat", lat);
                var lngParam = new SqlParameter("@lng", lng);
                var query = _db.Stores.FromSql("dbo.pGetThreeClosestStores @lat = {0}, @lng ={1}", lat, lng);
            storeDetails = query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return storeDetails;
        }
    }
}
