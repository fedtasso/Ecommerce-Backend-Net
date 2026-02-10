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

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",   // Vite dev
                "http://localhost:4173",   // Vite preview
                "http://localhost:3000",   // React dev
                "https://fedtasso-e-commerce-react-net.netlify.app"
            )
            .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS")
            .AllowAnyHeader()
            .AllowCredentials();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["ConnectionStrings__DefaultConnection"] // Render
    ?? throw new InvalidOperationException("Connection string not found");
    
    // ?? builder.Configuration["DATABASE_URL"];


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
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<CartMapper>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
    db.Database.Migrate(); // crea las tablas si no existen
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
