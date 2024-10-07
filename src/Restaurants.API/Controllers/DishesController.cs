using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId}/[controller]")]
[ApiController]
public class DishesController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
    {
        //command.RestaurantId = restaurantId;

        await mediator.Send(command);

        return Created();
    }
}
