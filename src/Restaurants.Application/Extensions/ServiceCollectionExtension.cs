using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using System.Reflection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtension
{

    public static void AddApplication(this IServiceCollection services)
    {
        Assembly? applicationAssembly = Assembly.GetExecutingAssembly();

        services.AddScoped<IRestaurantsService, RestaurantsService>();

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
    }
}
