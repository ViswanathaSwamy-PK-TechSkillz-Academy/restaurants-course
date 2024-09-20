using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
{

    public async Task<IEnumerable<RestaurantDto>> GetAll()
    {
        logger.LogInformation("Getting all restaurants");

        var restaurants = await restaurantsRepository.GetAllAsync();

        // Method 1
        //var restaurantsDto = restaurants.Select(restaurant => new RestaurantDto
        //{
        //    Id = restaurant.Id,
        //    Name = restaurant.Name,
        //    Description = restaurant.Description,
        //    Category = restaurant.Category,
        //    HasDelivery = restaurant.HasDelivery,
        //    City = restaurant.Address?.City,
        //    Street = restaurant.Address?.Street,
        //    PostalCode = restaurant.Address?.PostalCode,
        //});

        // Method 2
        var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);

        return restaurantsDto;
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant by {id}");

        var restaurant = await restaurantsRepository.GetByIdAsync(id);

        return restaurant;
    }
}
