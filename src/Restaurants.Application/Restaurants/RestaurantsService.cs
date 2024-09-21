using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
    ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
{

    public async Task<IEnumerable<RestaurantDto>> GetAll()
    {
        logger.LogInformation("Getting all restaurants");

        var restaurants = await restaurantsRepository.GetAllAsync();

        #region Method 1 and 2
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
        //var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);
        #region

        // Method 3
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return restaurantsDto!; // It will not be null. It might be empty collection.
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        logger.LogInformation("Getting restaurant by {id}", id);

        var restaurant = await restaurantsRepository.GetByIdAsync(id);

        var restaurantDto = restaurant == null ? null : RestaurantDto.FromEntity(restaurant);

        return restaurantDto;
    }
}
