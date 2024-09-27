namespace Restaurants.Application.Restaurants.Dtos;

public class CreateRestaurantDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactNumber { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? PostalCode { get; set; }
}

//public class CreateRestaurantDto
//{
//    [StringLength(100, MinimumLength = 3)]
//    public string Name { get; set; } = string.Empty;

//    public string Description { get; set; } = string.Empty;

//    [Required(ErrorMessage = "Insert a valid category.")]
//    public string Category { get; set; } = string.Empty;

//    public bool HasDelivery { get; set; }

//    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
//    public string? ContactEmail { get; set; }

//    [Phone(ErrorMessage = "Please provide a valid phone number")]
//    public string? ContactNumber { get; set; }

//    public string? City { get; set; }

//    public string? Street { get; set; }

//    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please provide a valid postal code")]
//    public string? PostalCode { get; set; }
//}

