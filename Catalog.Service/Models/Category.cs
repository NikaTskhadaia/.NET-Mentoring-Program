using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalog.Service.Models;

public class Category
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public List<Item> Items { get; set; } = default!; 
}
