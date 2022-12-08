using Cook.Masala.Models;
using Cook.Masala.Models.Enums;

namespace Cook.Masala.Cookers;

public class UkrainianCooker : Cooker
{
    
    protected override void SeasonRice()
    {
        _rice.Peppered = Level.Low;
        _rice.Salted = Level.Strongly;
    }

    protected override void FryRice()
    {
        _rice.Fried = Level.Strongly;
    }

    protected override void SeasonChicken()
    {
        _chicken.Salted = Level.Medium;
        _chicken.Peppered = Level.Low;
    }

    protected override void FryChicken()
    {
        _chicken.Fried = Level.Medium;
    }

    protected override void SetTeaIngredients()
    {
        _tea = new Tea(10, TeaColor.Black);
        _honey = new Honey(10);
    }

    protected override void SetRiceWeight()
    {
        _rice = new Rice(500);
    }

    protected override void SetChickenWeight()
    {
        _chicken = new Chicken(300);
    }
}