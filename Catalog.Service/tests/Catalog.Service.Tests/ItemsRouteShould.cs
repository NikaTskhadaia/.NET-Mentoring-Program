using Catalog.Service.Models;
using Catalog.Service.Tests.Fixtures;
using System.Net;
using System.Net.Http.Json;

namespace Catalog.Service.Tests;

public class ItemsRouteShould
{
    [Fact]
    public async void GetItems()
    {
        // Arrange
        await using var application = new ItemCatalogApplication();
        using var db = application.CreateItemDbContext();

        db.Items.Add(new Item { Description = "Brown Shoes"});
        await db.SaveChangesAsync();
        var client = application.CreateClient();

        // Act
        var items = await client.GetFromJsonAsync<List<Item>>("/items");

        // Assert
        Assert.NotNull(items);
        var item = Assert.Single(items);
        Assert.Equal("Brown Shoes", item.Description);
    }


    [Fact]
    public async Task PostTodos()
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
}