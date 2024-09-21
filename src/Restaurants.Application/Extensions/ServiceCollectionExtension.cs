using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
using System.Reflection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtension
{

    public static void AddApplication(this IServiceCollection services)
    {
        Assembly? applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IRestaurantsService, RestaurantsService>();

        // Explicitly register the AutoMapper profile(s)
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<RestaurantsProfile>(); // Explicitly add the RestaurantsProfile
        }, Assembly.GetExecutingAssembly()); // Still scanning the assembly in case there are other profiles
    }
}
