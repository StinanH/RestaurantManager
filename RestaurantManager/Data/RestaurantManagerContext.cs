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

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // To prevent cascading deletes when removing a Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Restaurant)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.FK_RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Table)
                .WithMany()
                .HasForeignKey(b => b.FK_TableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.FK_UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Timeslot)
                .WithMany()
                .HasForeignKey(b => b.FK_TimeslotId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Restaurant)
                .WithMany(r => r.Menus)
                .HasForeignKey(m => m.FK_RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.Menu)
                .WithMany(m => m.MenuItems)
                .HasForeignKey(mi => mi.FK_MenuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.CurrentOrders)
                .HasForeignKey(o => o.FK_RestaurantID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.FK_UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.MenuItems)
                .WithOne()   
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
                

            modelBuilder.Entity<User>().HasData
            (
                new User() { Id = 1, Name = "David Hedman", Email = "David@gmail.com", PhoneNumber = "1111111111" },
                new User() { Id = 2, Name = "Leo Horrorwitz", Email = "Leo@gmail.com", PhoneNumber = "1111111122" },
                new User() { Id = 3, Name = "Berend Mevius", Email = "Berend@gmail.com", PhoneNumber = "1111111133" },
                new User() { Id = 4, Name = "Siri Martinsson", Email = "Siri@gmail.com", PhoneNumber = "1111111144" }
            );

            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant() { Id = 1, Name = "Pazzi Pizza", Description = "Sveriges bästa pizzeria", Address = "Vällingbygatan 1, 16266 Vällingby", PhoneNumber = "0731111111", Email = "PazziPizza@gmail.com" },
                new Restaurant() { Id = 2, Name = "Café Kaffe", Description = "Världens bästa Café", Address = "Astrakangatan 1, 16552 Hässelby", PhoneNumber = "0732222222", Email = "CafeKaffe@gmail.com" }
            );

            modelBuilder.Entity<Menu>().HasData(
                new Menu() { Id = 1, FK_RestaurantId = 1, Name = "Meny" },
                new Menu() { Id = 2, FK_RestaurantId = 1, Name = "Lunchmeny" },
                new Menu() { Id = 3, FK_RestaurantId = 2, Name = "Meny" },
                new Menu() { Id = 4, FK_RestaurantId = 2, Name = "Helgmeny" }
                );

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem() { Id = 1, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Ananaspizza", Category = "Pizza", Description = "En sorts pizza.", isAvaliable = true, AmountAvaliable = 100},
                new MenuItem() { Id = 2, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Bananpizza", Category = "Pizza", Description = "En annan pizza.", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 3, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Bönpizza", Category = "Pizza", Description = "Också pizza.", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 4, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Pastapizza", Category = "Pizza", Description = "En Rund pizza.", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 5, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Cuba cola", Category = "Dryck", Description = "Lärre.", isAvaliable = true, AmountAvaliable = 100 },

                new MenuItem() { Id = 6, FK_MenuId = 2, FK_RestaurantId = 1, Name = "Pastasallad med banan", Category = "Pasta", Description = "Pasta med pålägg", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 7, FK_MenuId = 2, FK_RestaurantId = 1, Name = "Pastasallad med mint", Category = "Pasta", Description = "Pasta med annat pålägg", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 8, FK_MenuId = 2, FK_RestaurantId = 1, Name = "Pastasallad med lakrits", Category = "Pasta", Description = "Pasta med oätligt pålägg", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 9, FK_MenuId = 2, FK_RestaurantId = 1, Name = "Capritjosan", Category = "Pizza", Description = "Pizza med champinjoner", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 10, FK_MenuId = 2, FK_RestaurantId = 1, Name = "Margareta", Category = "Pizza", Description = "Pizza utan champinjoner", isAvaliable = true, AmountAvaliable = 100 },

                new MenuItem() { Id = 11, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Bulle", Category = "Bakelser", Description = "Snurrigt bakverk", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 12, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Kärleksrutor", Category = "Bakelser", Description = "Fyrkantigt bakverk", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 13, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Chokladboll", Category = "Bakelser", Description = "Sfäriskt bakverk", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 14, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Kaffe", Category = "Dryck", Description = "Brun dryck", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 15, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Té", Category = "Dryck", Description = "Halvgenomskinlig dryck", isAvaliable = true, AmountAvaliable = 100 },

                new MenuItem() { Id = 16, FK_MenuId = 4, FK_RestaurantId = 2, Name = "Bulle", Category = "Bakelser", Description = "Snurrigt bakverk", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 17, FK_MenuId = 4, FK_RestaurantId = 2, Name = "Hallonpaj med grädde", Category = "Bakelser", Description = "Rosa bakverk", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 18, FK_MenuId = 4, FK_RestaurantId = 2, Name = "Blåbärspaj med grädde", Category = "Bakelser", Description = "det är paj", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 19, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Kaffe", Category = "Dryck", Description = "Brun dryck", isAvaliable = true, AmountAvaliable = 100 },
                new MenuItem() { Id = 20, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Té", Category = "Dryck", Description = "Halvgenomskinlig dryck", isAvaliable = true, AmountAvaliable = 100 }
                );

        }
    }
}
