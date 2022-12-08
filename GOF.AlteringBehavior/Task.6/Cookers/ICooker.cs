using Cook.Masala.Models;
using Cook.Masala.Models.Enums;

namespace Cook.Masala.Cookers;

public interface ICooker
{
    void CookMasala();
    CupOfTea MakeTea();
    Rice CookRice();
    Chicken CookChicken();
}