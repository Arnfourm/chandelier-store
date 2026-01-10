using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Services;
using microservice.SupplyAPI.Infrastructure.Database.Contexts;
using microservice.SupplyAPI.Infrastructure.Database.DAO;
using microservices.SupplyAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

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


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Supply API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var connectionString = builder.Environment.IsDevelopment() 
    ? builder.Configuration.GetConnectionString("DevelopConnection")
    : builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SupplyDbContext>(
    options =>
    {
        options.UseNpgsql(connectionString);
    }
);

// DAO registration
builder.Services.AddScoped<IDeliveryTypeDAO, DeliveryTypeDAO>();
builder.Services.AddScoped<ISupplierDAO, SupplierDAO>();
builder.Services.AddScoped<ISupplyDAO, SupplyDAO>();
builder.Services.AddScoped<ISupplyProductDAO,  SupplyProductDAO>();

// Service registration
builder.Services.AddScoped<IDeliveryTypeService, DeliveryTypeService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISupplyService, SupplyService>();
builder.Services.AddScoped<ISupplyProductService, SupplyProductService>();
builder.Services.AddScoped<ISupplyDeleteService, SupplyDeleteService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
