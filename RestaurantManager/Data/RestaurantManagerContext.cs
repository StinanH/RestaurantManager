using Microsoft.EntityFrameworkCore;
using RestaurantManager.Models;

namespace RestaurantManager.Data
{
    public class RestaurantManagerContext : DbContext
    {
        public RestaurantManagerContext(DbContextOptions<RestaurantManagerContext> options) : base(options)
        {
            
        }

        //Dbsets
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData
            (
                new User() { Id = 1, Name = "David Hedman", Email = "David@gmail.com", PhoneNumber = 1111111111 },
                new User() { Id = 1, Name = "Leo Horrorwitz", Email = "Leo@gmail.com", PhoneNumber = 1111111122 },
                new User() { Id = 1, Name = "Berend Mevius", Email = "Berend@gmail.com", PhoneNumber = 1111111133 }
            );
        }
    }
}
