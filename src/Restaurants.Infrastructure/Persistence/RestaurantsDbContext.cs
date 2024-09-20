using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext : DbContext
{
    internal DbSet<Restaurant> Restaurants => Set<Restaurant>();

    internal DbSet<Dish> Dishes => Set<Dish>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=localhost;Database=RestaurantsDb;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }
}
