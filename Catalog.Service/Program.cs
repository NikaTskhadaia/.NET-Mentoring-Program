using Catalog.Service.Routes;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Items") ?? "Data Source=Items.db";
builder.Services.AddSqlite<ItemDbContext>(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().
    ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var itemsGroup = app.MapGroup("/items");
itemsGroup.MapItemRoutes();

var categoriesGroup = app.MapGroup("/categories");
categoriesGroup.MapCategoryRoutes();

app.Run();