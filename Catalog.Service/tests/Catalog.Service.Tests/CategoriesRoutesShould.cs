using Catalog.Service.Models;
using Catalog.Service.Tests.Fixtures;
using System.Net.Http.Json;
using System.Net;

namespace Catalog.Service.Tests;

public class CategoriesRoutesShould
{
    [Fact]
    public async void GetCategory()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();
        var newCategory = new Category { Name = "Drinks" };

        // Act
        db.Categories.Add(newCategory);
        await db.SaveChangesAsync();
        var category = await client.GetFromJsonAsync<Category>($"/categories/{newCategory.Id}");

        // Assert
        Assert.NotNull(category);
        Assert.Equal("Drinks", category!.Name);
    }

    [Fact]
    public async void GetCategories()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();
        var newCategory = new Category { Name = "Drinks" };

        // Act
        db.Categories.Add(newCategory);
        await db.SaveChangesAsync();
        var categories = await client.GetFromJsonAsync<List<Category>>("/categories");

        // Assert
        Assert.NotNull(categories);
        var category = Assert.Single(categories);
        Assert.Equal("Drinks", category.Name);
    }

    [Fact]
    public async Task PostCategory()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();
        var newCategory = new Category { Name = "Drinks" };

        // Act
        var response = await client.PostAsJsonAsync("/categories", newCategory);
        var categories = await client.GetFromJsonAsync<List<Category>>("/categories");

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(categories);
        var category = Assert.Single(categories);
        Assert.Equal("Drinks", category.Name);
        Assert.True(category.Items is null);
    }

    [Fact]
    public async Task UpdateCategory()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();
        var newCategory = new Category { Name = "Drinks", Items = Array.Empty<Item>() };

        // Act
        db.Categories.Add(newCategory);
        await db.SaveChangesAsync();
        newCategory.Name = "Updated";
        var response = await client.PutAsJsonAsync($"categories/{newCategory.Id}", newCategory);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var updatedCategory = await client.GetFromJsonAsync<Category>($"/categories/{newCategory.Id}");
        Assert.NotNull(updatedCategory);
        Assert.Equal("Updated", updatedCategory?.Name);
    }

    [Fact]
    public async Task DeleteCategory()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();
        var newCategory = new Category { Name = "Drinks" };

        // Act
        db.Categories.Add(newCategory);
        await db.SaveChangesAsync();
        var dbCategory = db.Categories.FirstOrDefault(i => i.Id == newCategory.Id);

        // Assert
        Assert.NotNull(dbCategory);
        Assert.Equal("Drinks", dbCategory?.Name);
        Assert.True(dbCategory?.Items is null);

        // Act
        var response = await client.DeleteAsync($"/categories/{dbCategory.Id}");
        var deletedCategory = db.Items.FirstOrDefault(i => i.Id == dbCategory.Id);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Null(deletedCategory);
    }
}
