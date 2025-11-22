using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Services;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.DAO;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DevelopConnection"));
    }
);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
