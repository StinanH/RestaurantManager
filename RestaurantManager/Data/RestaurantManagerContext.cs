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

            // To prevent cascading deletes for certain objects

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
                new User() { Id = 4, Name = "Siri Martinsson", Email = "Siri@gmail.com", PhoneNumber = "1111111144" },
                new User() { Id = 5, Name = "Adam Min", Email= "Admin@gmail.com", PhoneNumber = "0707070707"},
                new User() { Id = 6, Name = "Userella De Fault", Email = "User@gmail.com", PhoneNumber= "0737373737"}
            );

            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant() { Id = 1, Name = "Pazzi Pizza", Description = "Sveriges bästa pizzeria", Address = "Vällingbygatan 1, 16266 Vällingby", PhoneNumber = "0731111111", Email = "PazziPizza@gmail.com" },

            //only using restaurant with id 1 atm. below not in use.
                new Restaurant() { Id = 2, Name = "Café Kaffe", Description = "Världens bästa Café", Address = "Astrakangatan 1, 16552 Hässelby", PhoneNumber = "0732222222", Email = "CafeKaffe@gmail.com" }
            );

            modelBuilder.Entity<Menu>().HasData(
                new Menu() { Id = 1, FK_RestaurantId = 1, Name = "Meny" },

            //only using restaurant with id 1 atm. below not in use.
                new Menu() { Id = 2, FK_RestaurantId = 2, Name = "Meny" },
                new Menu() { Id = 3, FK_RestaurantId = 2, Name = "Helgmeny" }
                );

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem() { Id = 1, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Ananaspizza", Category = "Pizza", Description = "En sorts pizza.", IsAvailable = true, Price = 90, AmountSold = 70 },
                new MenuItem() { Id = 2, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Bananpizza", Category = "Pizza", Description = "En annan pizza.", IsAvailable = true, Price = 80, AmountSold = 90 },
                new MenuItem() { Id = 3, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Bönpizza", Category = "Pizza", Description = "Också pizza.", IsAvailable = true, Price = 70, AmountSold = 80 },
                new MenuItem() { Id = 4, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Pastapizza", Category = "Pizza", Description = "En Rund pizza.", IsAvailable = true, Price = 60, AmountSold = 40 },
                new MenuItem() { Id = 5, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Cuba cola", Category = "Dryck", Description = "Lärre.", IsAvailable = true, Price = 50, AmountSold = 40 },
                new MenuItem() { Id = 6, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Pastasallad med banan", Category = "Pasta", Description = "Pasta med pålägg", IsAvailable = true, Price = 40, AmountSold = 50 },
                new MenuItem() { Id = 7, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Pastasallad med mint", Category = "Pasta", Description = "Pasta med annat pålägg", IsAvailable = true, Price = 45, AmountSold = 70 },
                new MenuItem() { Id = 8, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Pastasallad med lakrits", Category = "Pasta", Description = "Pasta med oätligt pålägg", IsAvailable = true, Price = 45, AmountSold = 90 },
                new MenuItem() { Id = 9, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Capritjosan", Category = "Pizza", Description = "Pizza med champinjoner", IsAvailable = true, Price = 70, AmountSold = 50 },
                new MenuItem() { Id = 10, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Margareta", Category = "Pizza", Description = "Pizza utan champinjoner", IsAvailable = true, Price = 71, AmountSold = 56 },
                new MenuItem() { Id = 11, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Kaffe", Category = "Dryck", Description = "Brun dryck", IsAvailable = true, Price = 10, AmountSold = 33 },
                new MenuItem() { Id = 12, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Té", Category = "Dryck", Description = "Halvgenomskinlig dryck", IsAvailable = true, Price = 10, AmountSold = 6 },
                new MenuItem() { Id = 13, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Hallonsoda", Category = "Dryck", Description = "Rosa lärre", IsAvailable = true, Price = 20, AmountSold = 4},
                new MenuItem() { Id = 14, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Lemonad", Category = "Dryck", Description = "Gul lärre", IsAvailable = true, Price = 20, AmountSold = 4 },
                new MenuItem() { Id = 15, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Zingo", Category = "Dryck", Description = "Orange lärre", IsAvailable = true, Price = 15, AmountSold = 30 },
                new MenuItem() { Id = 16, FK_MenuId = 1, FK_RestaurantId = 1, Name = "Cola", Category = "Dryck", Description = "Annan brun dryck", IsAvailable = true, Price = 15, AmountSold = 20 },

            //only using restaurant with id 1 atm. below not in use.
                new MenuItem() { Id = 17, FK_MenuId = 2, FK_RestaurantId = 2, Name = "Té", Category = "Dryck", Description = "Halvgenomskinlig dryck", IsAvailable = true, Price = 100, AmountSold = 0 },

                new MenuItem() { Id = 18, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Bulle", Category = "Bakelser", Description = "Snurrigt bakverk", IsAvailable = true, Price = 100, AmountSold = 0 },
                new MenuItem() { Id = 19, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Hallonpaj med grädde", Category = "Bakelser", Description = "Rosa bakverk", IsAvailable = true, Price = 100, AmountSold = 0 },
                new MenuItem() { Id = 20, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Blåbärspaj med grädde", Category = "Bakelser", Description = "det är paj", IsAvailable = true, Price = 100, AmountSold = 0 },
                new MenuItem() { Id = 21, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Kaffe", Category = "Dryck", Description = "Brun dryck", IsAvailable = true, Price = 100, AmountSold = 0 },
                new MenuItem() { Id = 22, FK_MenuId = 3, FK_RestaurantId = 2, Name = "Té", Category = "Dryck", Description = "Halvgenomskinlig dryck", IsAvailable = true, Price = 100, AmountSold = 0 }
                );

            modelBuilder.Entity<Account>().HasData(
                new Account() { Id = 1, Email = "Admin@gmail.com", FK_User = 5, isAdmin = true, PasswordHashed = BCrypt.Net.BCrypt.HashPassword("AdminPassword123")},
                new Account() { Id = 2, Email = "User@gmail.com", FK_User = 6, isAdmin = false, PasswordHashed = BCrypt.Net.BCrypt.HashPassword("UserPassword123")}
                );
        }
    }
}
