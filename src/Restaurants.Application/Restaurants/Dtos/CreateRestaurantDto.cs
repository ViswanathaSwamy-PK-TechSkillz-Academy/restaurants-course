using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Dtos;

public class CreateRestaurantDto
{
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Insert a valid category.")]
    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    [EmailAddress]
    public string? ContactEmail { get; set; }

    public string? ContactNumber { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? PostalCode { get; set; }
}
