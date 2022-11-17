namespace Catalog.Service.Models;

public class Category
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public IEnumerable<Item> Items { get; set; } = default!; 
}
