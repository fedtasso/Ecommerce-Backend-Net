using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Application.Mappers;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Security;
using Ecommerce.Api.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["DATABASE_URL"];


builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        b => b.MigrationsAssembly("Ecommerce.Infrastructure")
    )
);
// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

//Services
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ProductMapper>();


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
