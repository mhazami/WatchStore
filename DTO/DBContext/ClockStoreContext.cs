using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO.DBContext
{
    public class ClockStoreContext : DbContext
    {
        public ClockStoreContext() : base("name=ClockStoreConnectionString")
        { }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<AboutUs> AboutUs { get; set; }
        public virtual DbSet<Basket> Basket { get; set; }

        public virtual DbSet<Cantact> Cantact { get; set; }

        public virtual DbSet<Category> Category { get; set; }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<WallPaper> WallPaper { get; set; }

        public virtual DbSet<News> News { get; set; }

        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }

        public virtual DbSet<ExtraImages> ExtraImages { get; set; }

        public virtual DbSet<Language> Language { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }




    }
}
