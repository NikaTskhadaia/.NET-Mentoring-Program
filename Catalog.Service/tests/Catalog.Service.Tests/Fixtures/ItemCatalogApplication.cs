using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Catalog.Service.Tests.Fixtures;

internal class ItemCatalogApplication : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _sqliteConnection = new("Filename=:memory:");

    public ItemDbContext CreateItemDbContext()
    {
        var db = Services.GetRequiredService<IDbContextFactory<ItemDbContext>>().CreateDbContext();
        db.Database.EnsureCreated();
        return db;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Open the connection, this creates the SQLite in-memory database, which will persist until the connection is closed
        _sqliteConnection.Open();

        builder.ConfigureServices(services =>
        {
            // We're going to use the factory from our tests
            services.AddDbContextFactory<ItemDbContext>();

            // We need to replace the configuration for the DbContext to use a different configured database
            services.AddDbContextOptions<ItemDbContext>(o => o.UseSqlite(_sqliteConnection));
        });

        return base.CreateHost(builder);
    }
}
