using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Infrastructure.Database.DAO;
using microservices.UserAPI.Domain.Services;
using microservices.UserAPI.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

// DAO registration
builder.Services.AddScoped<IUserDAO, UserDAO>();
builder.Services.AddScoped<IClientDAO, ClientDAO>();
builder.Services.AddScoped<IEmployeeDAO, EmployeeDAO>();
builder.Services.AddScoped<IFavouritesDAO, FavouritesDAO>();
builder.Services.AddScoped<IPasswordDAO, PasswordDAO>();
builder.Services.AddScoped<IRefreshTokenDAO, RefreshTokenDAO>();

// Service registration
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
