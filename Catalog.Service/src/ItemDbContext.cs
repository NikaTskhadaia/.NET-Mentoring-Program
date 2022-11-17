using Catalog.Service.Models;
using Microsoft.EntityFrameworkCore;

public class ItemDbContext : DbContext
{
	public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options) { }

	public DbSet<Item> Items { get; set; }
	public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Item>()
			.HasKey(i => i.Id);

		modelBuilder.Entity<Category>()
			.HasKey(i => i.Id);

		modelBuilder.Entity<Category>()
			.HasMany(item => item.Items)
			.WithOne(i => i.Category);

		modelBuilder.Entity<Category>()
			.Property(i => i.Name)
			.IsRequired();

		modelBuilder.Entity<Item>()
			.Property(i => i.Description)
			.IsRequired();
    }
}