namespace Catalog.Service.Models;

public class Item
{
    public int Id { get; set; }

    public string Description { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}
