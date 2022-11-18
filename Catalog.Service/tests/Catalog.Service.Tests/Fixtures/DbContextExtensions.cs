using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Catalog.Service.Tests.Fixtures;

internal static class DbContextExtensions
{
    public static IServiceCollection AddDbContextOptions<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder<TContext>> configure) where TContext : DbContext
    {
        // Remove the existing DbContextOptions
        services.RemoveAll(typeof(DbContextOptions<TContext>));

        // Add the DbContextOptions<TContext>
        services.AddSingleton(s =>
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<TContext>();

            configure(dbContextOptionsBuilder);

            return dbContextOptionsBuilder.Options;
        });

        // The untyped version just calls the typed one
        services.AddSingleton<DbContextOptions>(s => s.GetRequiredService<DbContextOptions<TContext>>());

        return services;
    }
}
