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

            var item = new Item
            {
                Description = newItem.Description,
                CategoryId = newItem.CategoryId
            };

            await db.Items.AddAsync(item);
            await db.SaveChangesAsync();

            return Results.Created($"/items/{item.Id}", item);
        })
        .Produces(Status201Created)
        .ProducesValidationProblem();

        group.MapPut("/{id}", async (ItemDbContext db, int id, Item newItem) =>
        {
            var item = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item is null)
            {
                return Results.NotFound();
            }
            item.Description = newItem.Description;
            item.CategoryId = newItem.CategoryId;

            db.Update(item);
            await db.SaveChangesAsync();

            return Results.Ok();
        })
        .Produces(Status404NotFound)
        .Produces(Status204NoContent);

        group.MapDelete("/{id}", async (ItemDbContext db, int id) =>
        {
            var item = await db.Items.FindAsync(id);
            if (item is null)
            {
                return Results.NotFound();
            }

            db.Items.Remove(item);
            await db.SaveChangesAsync();

            return Results.Ok();
        })
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
        {
            var category = await db.Categories.FindAsync(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            category.Name = newCategory.Name;
            category.Items = category.Items;

            db.Update(category);
            await db.SaveChangesAsync();

            return Results.Ok();
        })
        .Produces(Status404NotFound)
        .Produces(Status204NoContent);

        group.MapDelete("/{id}", async (ItemDbContext db, int id) =>
        {
            var category = await db.Categories.FindAsync(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            return Results.Ok();
        })
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
