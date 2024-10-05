using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Infrastructure.Seeders;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// TODO: Use Strongly Type Configuration Object
builder.Services.AddPresentation(builder.Configuration);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

WebApplication app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seeding the database
    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
    await seeder.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
