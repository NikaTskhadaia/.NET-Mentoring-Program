using Cook.Masala.Cookers;
using Cook.Masala.Models;
using Cook.Masala.Models.Enums;

namespace Cook.Masala;

public class Restaurant
{
    public void CookMasala(ICooker cooker, Country country)
    {
        cooker.CookMasala();
    }
}