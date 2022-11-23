using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Catalog.Service.Routes;

internal static class Routes
{
    public static RouteGroupBuilder MapItemRoutes(this RouteGroupBuilder group)
    {
        group.WithTags("Items");

        group.MapGet("/", ItemsWithPagination)
        .Produces<List<Item>>();

        group.MapGet("/{id}", async (ItemDbContext db, int id) =>
        {
            return await db.Items.FindAsync(id) switch
            {
                Item item => Results.Ok(item),
                _ => Results.NotFound()
            };
        })
        .Produces<Item>()
        .Produces(Status404NotFound);

        group.MapPost("/", async (ItemDbContext db, Item newItem) =>
        {
            if (string.IsNullOrEmpty(newItem.Description))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    ["Description"] = new[] { "A description is required" }
                });
            }

            await db.Items.AddAsync(newItem);
            await db.SaveChangesAsync();

            return Results.Created($"/items/{newItem.Id}", newItem);
        })
        .Produces(Status201Created)
        .ProducesValidationProblem();

        group.MapPut("/{id}", async (ItemDbContext db, int id, Item newItem) =>        
            await db.Items.Where(x => x.Id == id).ExecuteUpdateAsync(
                updates => updates
                    .SetProperty(i => i.Description, newItem.Description)
                    .SetProperty(i => i.CategoryId, newItem.CategoryId)) == 0
                ? Results.NotFound()
                : Results.NoContent())
        .Produces(Status404NotFound)
        .Produces(Status204NoContent);

        group.MapDelete("/{id}", async (ItemDbContext db, int id) =>
            await db.Items.Where(item => item.Id == id).ExecuteDeleteAsync() == 0
            ? Results.NotFound()
            : Results.NoContent())
        .Produces(Status404NotFound)
        .Produces(Status204NoContent);

        return group;
    }

    public static RouteGroupBuilder MapCategoryRoutes(this RouteGroupBuilder group)
    {
        group.WithTags("Categories");

        group.MapGet("/", async (ItemDbContext db) =>
        {
            return await db.Categories.ToListAsync();
        })
        .Produces<List<Category>>();

        group.MapGet("/{id}", async (ItemDbContext db, int id) =>
        {
            return await db.Categories.FindAsync(id) switch
            {
                Category category => Results.Ok(category),
                _ => Results.NotFound()
            };
        })
        .Produces<Category>()
        .Produces(Status404NotFound);

        group.MapPost("/", async (ItemDbContext db, Category newCategory) =>
        {
            if (string.IsNullOrEmpty(newCategory.Name))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    ["Name"] = new[] { "A name is required" }
                });
            }

            var category = new Category
            {
                Name = newCategory.Name
            };

            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();

            return Results.Created($"/items/{category.Id}", category);
        })
        .Produces(Status201Created)
        .ProducesValidationProblem();

        group.MapPut("/{id}", async (ItemDbContext db, int id, Category newCategory) =>
            await db.Categories.Where(c => c.Id == id).ExecuteUpdateAsync(
                updates => updates
                    .SetProperty(c => c.Name, newCategory.Name)) == 0
                ? Results.NotFound()
                : Results.NoContent())
        .Produces(Status404NotFound)
        .Produces(Status204NoContent);

        group.MapDelete("/{id}", async (ItemDbContext db, int id) =>
            await db.Categories.Where(c => c.Id == id).ExecuteDeleteAsync() == 0
            ? Results.NotFound()
            : Results.NoContent())
        .Produces(Status404NotFound)
        .Produces(Status204NoContent);

        return group;
    }

    private static async Task<List<Item>> ItemsWithPagination(ItemDbContext db, int? categoryId, int pageNumber = 1, int pageSize = 20)
    {
        return await db.Items
            .Where(i => !categoryId.HasValue || i.CategoryId == categoryId)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();        
    }
}
