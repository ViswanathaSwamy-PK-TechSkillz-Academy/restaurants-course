﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("RestaurantsDbConnection");
        services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            }).EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();

        services.AddScoped<IDishesRepository, DishesRepository>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Indian", "Polish"))
            .AddPolicy(PolicyNames.AtLeast20,
                builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
        .AddPolicy(PolicyNames.CreatedAtleast2Restaurants,
            builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();

        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
    }

}
