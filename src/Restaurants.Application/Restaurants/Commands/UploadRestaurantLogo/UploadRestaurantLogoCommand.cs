using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;

public class UploadRestaurantLogoCommand : IRequest
{
    public int RestaurantId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public Stream File { get; set; } = default!;
}