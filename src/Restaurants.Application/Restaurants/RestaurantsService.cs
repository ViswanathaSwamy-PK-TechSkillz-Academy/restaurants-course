using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService
{

    public async Task<IEnumerable<Restaurant>> GetAll()
    {
        var restaurants = await mediator.Send(query);
        return restaurants;
    }
}
