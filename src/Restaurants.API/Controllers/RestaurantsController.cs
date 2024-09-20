using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetAll()
    {
        var restaurants = await mediator.Send(query);

        return Ok(restaurants);
    }
}
