using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace ASPNetExercises.Models
{

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().ForSqlServerToTable("Categories")
                .Property(c => c.Id).UseSqlServerIdentityColumn();
            builder.Entity<MenuItem>().ForSqlServerToTable("Menuitems")
                .Property(m => m.Id).UseSqlServerIdentityColumn();
            builder.Entity<Tray>().ForSqlServerToTable("Trays")
                .Property(t => t.Id).UseSqlServerIdentityColumn();
            builder.Entity<TrayItem>().ForSqlServerToTable("TrayItems")
                .Property(ti => ti.Id).UseSqlServerIdentityColumn();
            builder.Entity<Store>().ForSqlServerToTable("Stores")
                .Property(s => s.Id).UseSqlServerIdentityColumn();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;AttachDbFilename=D:\\College\\Term 4\\INFO 3067\\$Workspace 3067\\ASPNetExercisesDB\\exercisesDb.mdf;Integrated Security=True;Connect Timeout=10");
        //}


        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tray> Trays { get; set; }
        public virtual DbSet<TrayItem> TrayItems { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
    }
}
