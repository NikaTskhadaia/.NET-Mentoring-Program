using Catalog.Service.Models;
using Catalog.Service.Tests.Fixtures;
using System.Net;
using System.Net.Http.Json;

namespace Catalog.Service.Tests;

public class ItemsRouteShould
{
    [Fact]
    public async void GetItem()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();
        var newItem = new Item { Description = "Brown Shoes" };

        // Act
        db.Items.Add(newItem);
        await db.SaveChangesAsync();
        var item = await client.GetFromJsonAsync<Item>($"/items/{newItem.Id}");

        // Assert
        Assert.NotNull(item);
        Assert.Equal("Brown Shoes", item!.Description);
    }

    [Fact]
    public async void GetItems()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();

        // Act
        db.Items.Add(new Item { Description = "Brown Shoes" });
        await db.SaveChangesAsync();
        var items = await client.GetFromJsonAsync<List<Item>>("/items");

        // Assert
        Assert.NotNull(items);
        var item = Assert.Single(items);
        Assert.Equal("Brown Shoes", item.Description);
    }

    [Fact]
    public async Task PostItem()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/items", new Item { Description = "Custom Keyboard" });
        var items = await client.GetFromJsonAsync<List<Item>>("/items");

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(items);
        var item = Assert.Single(items);
        Assert.Equal("Custom Keyboard", item.Description);
        Assert.False(item.CategoryId.HasValue);
    }

    [Fact]
    public async Task UpdateItem()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();
        var item = new Item { Description = "JBL 3" };

        // Act
        db.Items.Add(item);
        await db.SaveChangesAsync();
        item.Description = "Updated";
        var response = await client.PutAsJsonAsync($"items/{item.Id}", item);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var updatedItem = await client.GetFromJsonAsync<Item>($"/items/{item.Id}");
        Assert.NotNull(updatedItem);
        Assert.Equal("Updated", updatedItem?.Description);
    }

    [Fact]
    public async Task DeleteItem()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();
        var client = application.CreateClient();
        var item = new Item { Description = "Coca-Cola" };

        // Act
        db.Items.Add(item);
        await db.SaveChangesAsync();
        var dbItem = db.Items.FirstOrDefault(i => i.Id == item.Id);

        // Assert
        Assert.NotNull(item);
        Assert.Equal("Coca-Cola", item?.Description);
        Assert.False(item?.CategoryId.HasValue);

        // Act
        var response = await client.DeleteAsync($"/items/{item.Id}");
        item = db.Items.FirstOrDefault(i => i.Id == item.Id);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Null(item);
    }
}