using Microsoft.Extensions.Caching.Memory;
using Trip.Insurance.Calculation.Calculators;

namespace Trip.Insurance.Calculation.CalculatorDecorators;

public class CalculatorWithCaching : CalculatorDecorator
{
    private readonly IMemoryCache _cache;

    public CalculatorWithCaching(ICalculator paymentCalculator, IMemoryCache cache) : base(paymentCalculator)
    {
        _cache = cache;
    }

    public override decimal CalculatePayment(string touristName)
    {
        if (_cache.TryGetValue(touristName, out decimal cachedPayment))
        {
            return cachedPayment;
        }

        var payment = base.CalculatePayment(touristName);
        _cache.Set(touristName, payment);
        return payment;
    }
}