using Cook.Masala.Models;
using Cook.Masala.Models.Enums;

namespace Cook.Masala.Cookers;

public class IndianCooker : Cooker
{
    protected override void SeasonRice()
    {
        _rice.Peppered = Level.Strongly;
        _rice.Salted = Level.Strongly;
    }

    protected override void FryRice()
    {
        _rice.Fried = Level.Strongly;
    }

    protected override void SeasonChicken()
    {
        _chicken.Salted = Level.Strongly;
        _chicken.Peppered = Level.Strongly;
    }

    protected override void FryChicken()
    {
        _chicken.Fried = Level.Strongly;
    }

    protected override void SetTeaIngredients()
    {
        _tea = new Tea(15, TeaColor.Green);
        _honey = new Honey(12);
    }

    protected override void SetRiceWeight()
    {
        _rice = new Rice(200);
    }

    protected override void SetChickenWeight()
    {
        _chicken = new Chicken(100);
    }
}