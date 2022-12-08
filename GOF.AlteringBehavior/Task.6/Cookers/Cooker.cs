using Cook.Masala.Models;
using Cook.Masala.Models.Enums;

namespace Cook.Masala.Cookers;

public abstract class Cooker : ICooker
{
    protected Rice _rice;
    protected Chicken _chicken;
    protected Tea _tea;
    protected Honey _honey;
    
    protected abstract void SeasonRice();
    protected abstract void FryRice();
    protected abstract void SeasonChicken();
    protected abstract void FryChicken();
    protected abstract void SetTeaIngredients();
    protected abstract void SetRiceWeight();
    protected abstract void SetChickenWeight();

    public void CookMasala()
    {
        Console.WriteLine("Cooking Rice...\n");
        CookRice();
        Console.WriteLine("Rice is cooked...\n");
        Console.WriteLine("Cooking chicken...\n");
        CookChicken();
        Console.WriteLine("Chicken is cooked...\n");
        Console.WriteLine("Making tea...\n");
        MakeTea();
        Console.WriteLine("Tea is made...\n");
    }

    public CupOfTea MakeTea()
    {
        SetTeaIngredients();
        return new CupOfTea(_tea, _honey);
    }
    
    public Rice CookRice()
    {
        SetRiceWeight();
        SeasonRice();
        FryRice();
        return _rice;
    }

    public Chicken CookChicken()
    {
        SetChickenWeight();
        SeasonChicken();
        FryChicken();
        return _chicken;
    }
}