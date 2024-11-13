using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;
using System.Reflection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtension
{

    public static void AddApplication(this IServiceCollection services)
    {
        Assembly? applicationAssembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }
}
