using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto>
{
    public int Id { get; } = id;
}