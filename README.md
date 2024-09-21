# Restaurants Web API in .NET 8 (Clean Architecture)

I am learning .NET Web API using Clean Architecture from different Video Courses, Books, and Websites.

## Reference(s)

> 1. <https://www.udemy.com/course/aspnet-core-web-api-clean-architecture-azure>
> 1. <https://www.dandoescode.com/blog/clean-architecture-an-introduction>
> 1. <https://blog.ndepend.com/clean-architecture-for-asp-net-core-solution/>

## Few Commands
```powershell
dotnet tool update --global dotnet-ef

D:\TSA\restaurants-course\src\Restaurants.Infrastructure

# Add migration for RestaurantsDbContext
dotnet ef migrations add InitialCreate --context RestaurantsDbContext --project . --startup-project ..\Restaurants.API

# Update database for RestaurantsDbContext
dotnet ef database update --context RestaurantsDbContext --project . --startup-project ..\Restaurants.API
```