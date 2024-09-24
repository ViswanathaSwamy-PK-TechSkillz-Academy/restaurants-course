using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await restaurantsService.GetAll();

        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var restaurant = await restaurantsService.GetById(id);
        if (restaurant == null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        var id = await restaurantsService.Create(createRestaurantDto);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

}
