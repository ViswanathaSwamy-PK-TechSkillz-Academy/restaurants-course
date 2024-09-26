using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Dtos;

public class CreateRestaurantDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactNumber { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? PostalCode { get; set; }
}
