using Cook.Masala.Models.Enums;

namespace Cook.Masala.Models;

public interface IIngredient
{
    public double Weight { get; set; }
    public Level? Fried { get; set; }
    public Level? Salted { get; set; }
    public Level? Peppered { get; set; }
}