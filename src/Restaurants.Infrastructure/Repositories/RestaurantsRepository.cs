﻿using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();

        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var restaurants = dbContext
            .Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                   || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await restaurants.CountAsync();

        //if (sortBy != null)
        //{
        //    var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
        //    {
        //        { nameof(Restaurant.Name), r => r.Name },
        //        { nameof(Restaurant.Description), r => r.Description },
        //        { nameof(Restaurant.Category), r => r.Category },
        //    };

        //    var selectedColumn = columnsSelector[sortBy];

        //    baseQuery = sortDirection == SortDirection.Ascending
        //        ? baseQuery.OrderBy(selectedColumn)
        //        : baseQuery.OrderByDescending(selectedColumn);
        //}

        //var restaurants = await baseQuery
        //    .Skip(pageSize * (pageNumber - 1))
        //    .Take(pageSize)
        //    .ToListAsync();

        return (restaurants, totalCount);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);

        return restaurant;
    }

    public async Task<int> CreateAsync(Restaurant restaurant)
    {
        await dbContext.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();

        return restaurant.Id;
    }

    public async Task DeleteAsync(Restaurant restaurant)
    {
        dbContext.Remove(restaurant);

        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}
