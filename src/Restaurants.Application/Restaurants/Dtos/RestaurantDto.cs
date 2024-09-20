using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    public string City { get; set; } = "Some City";

    public string Street { get; set; } = "Some Street";

    public string PostalCode { get; set; } = "10001";

    public List<DishDto> Dishes { get; set; } = [];
}