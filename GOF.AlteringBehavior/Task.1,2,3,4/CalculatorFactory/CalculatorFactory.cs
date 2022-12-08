using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Trip.Insurance.Calculation.CalculatorDecorators;
using Trip.Insurance.Calculation.Calculators;
using Trip.Insurance.Calculation.Infrastructure.CurrencyService;
using Trip.Insurance.Calculation.Infrastructure.TripRepository;

namespace Trip.Insurance.Calculation.CalculatorFactory;

public class CalculatorFactory : ICalculatorFactory
{
    public ICalculator CreateCalculator()
    {
        return new InsurancePaymentCalculator(new CurrencyService(), new TripRepository());
    }

    public ICalculator CreateCachedCalculator()
    {
        return new CalculatorWithCaching(
            CreateCalculator(), 
            new MemoryCache(new MemoryCacheOptions()));
    }

    public ICalculator CreateLoggingCalculator()
    {
        return new CalculatorWithLogging(
            CreateCalculator(),
            new LoggerFactory().CreateLogger<CalculatorWithLogging>());
    }

    public ICalculator CreateRoundingCalculator()
    {
        return new CalculatorWithRounding(CreateCalculator());
    }

    public void SetCalculatorDecorator(ICalculator decorator, ICalculator calculator)
    {
        ArgumentNullException.ThrowIfNull(decorator);
        ArgumentNullException.ThrowIfNull(calculator);
        (decorator as CalculatorDecorator)!.SetCalculator(calculator);
    }
}