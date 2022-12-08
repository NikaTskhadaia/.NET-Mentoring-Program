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

    public ICalculator CreateCachedInsurancePaymentCalculator()
    {
        return new InsurancePaymentCalculatorWithCaching(
            CreateCalculator(), 
            new MemoryCache(new MemoryCacheOptions()));
    }

    public ICalculator CreateLoggingInsurancePaymentCalculator()
    {
        return new InsurancePaymentCalculatorWithLogging(
            CreateCalculator(),
            new LoggerFactory().CreateLogger<InsurancePaymentCalculatorWithLogging>());
    }

    public ICalculator CreateRoundingInsurancePaymentCalculator()
    {
        return new InsurancePaymentCalculatorWithRounding(CreateCalculator());
    }

    public ICalculator CreateInsurancePaymentCalculatorWithDecorator(ICalculator calculator)
    {
        return new InsurancePaymentCalculatorWithDecorators(calculator);
    }
}