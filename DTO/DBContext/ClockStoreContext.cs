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

        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<BasketOrder> BasketOrder { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VideoHandler> VideoHandler { get; set; }
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

        public virtual DbSet<UserComments> UserComments { get; set; }

        public virtual DbSet<Clock> Clocks { get; set; }
        public virtual DbSet<Desktop> Desktops{ get; set; }

        public virtual DbSet<OfferCard>OfferCards { get; set; }

        public virtual DbSet<CustomerOffer> CustomerOffers { get; set; }

        public virtual DbSet<OfferBanner> OfferBanners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<ClockStore.DTO.Advertise> Advertises { get; set; }
    }
}
