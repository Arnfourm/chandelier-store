using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Services;
using microservice.SupplyAPI.Infrastructure.Database.Contexts;
using microservice.SupplyAPI.Infrastructure.Database.DAO;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
