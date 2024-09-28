using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
{
}
