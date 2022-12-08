using Cook.Masala.Models.Enums;

namespace Cook.Masala.Models;

public class Rice : IIngredient
{
    public Rice(double weight)
    {
        Weight = weight;
    }

    public double Weight { get; set; }
    public Level? Fried { get; set; }
    public Level? Salted { get; set; }
    public Level? Peppered { get; set; }
}