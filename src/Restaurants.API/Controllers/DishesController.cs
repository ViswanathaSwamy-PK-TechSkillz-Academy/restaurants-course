using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId}/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
}
