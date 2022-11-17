namespace Catalog.Service.Models;

public class Item
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    public int? CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; } = default!;
}
