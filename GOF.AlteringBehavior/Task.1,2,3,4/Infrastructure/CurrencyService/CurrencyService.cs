namespace Trip.Insurance.Calculation.Infrastructure.CurrencyService;

public class CurrencyService : ICurrencyService
{
    public decimal LoadCurrencyRate()
    {
        return decimal.One;
    }
}