using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options)
    : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants => Set<Restaurant>();

    internal DbSet<Dish> Dishes => Set<Dish>();

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    // ***** A BIG NO-NO! This is just for demo purposes. *****
    //    //string connectionString = "Server=(localdb)\\mssqllocaldb;Database=RestaurantsDb;Trusted_Connection=True;MultipleActiveResultSets=true";
    //    //optionsBuilder.UseSqlServer(connectionString);
    //    // ***** A BIG NO-NO! This is just for demo purposes. *****
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);

        modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedRestaurants)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);

        modelBuilder.Entity<Dish>()
            .Property(d => d.Price)
            .HasPrecision(18, 4); // Specify precision and scale
    }
}
