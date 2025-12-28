using microservices.ReviewAPI.Domain.Interfaces.DAO;
using microservices.ReviewAPI.Domain.Interfaces.Services;
using microservices.ReviewAPI.Domain.Services;
using microservices.ReviewAPI.Infrastructure.Database.Contexts;
using microservices.ReviewAPI.Infrastructure.Database.DAO;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DevelopConnection")
    : builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ReviewDbContext>(
    options =>
    {
        options.UseNpgsql(connectionString);
    }
);

// DAO registration
builder.Services.AddScoped <IReviewDAO, ReviewDAO>();

// Service registration
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
