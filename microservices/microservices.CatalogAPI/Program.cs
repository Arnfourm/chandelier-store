using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Services;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var secretKey = builder.Configuration["JWTSecretKey"]
    ?? throw new ArgumentNullException("JWTSecretKey");


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "microservices.UserAPI",
        ValidAudience = "microservices.Client",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();



var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DevelopConnection")
    : builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Allow Frontend-requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Allow Frontend-requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// DAO registry
builder.Services.AddScoped<IProductTypeDAO, ProductTypeDAO>();
builder.Services.AddScoped<IProductDAO, ProductDAO>();
builder.Services.AddScoped<IAttributeGroupDAO, AttributeGroupDAO>();
builder.Services.AddScoped<IMeasurementUnitDAO, MeasurementUnitDAO>();
builder.Services.AddScoped<IAttributeDAO, AttributeDAO>();
builder.Services.AddScoped<IProductAttributeDAO, ProductAttributeDAO>();

// Services registry
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAttributeGroupService, AttributeGroupService>();
builder.Services.AddScoped<IMeasurementUnitService, MeasurementUnitService>();
builder.Services.AddScoped<IAttributeService, AttributeService>();
builder.Services.AddScoped<IProductAttributeService, ProductAttributeService>();
builder.Services.AddScoped<IDeleteProductAttributeService, DeleteProductAttributeService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
