using Trip.Insurance.Calculation.CalculatorDecorators;
using Trip.Insurance.Calculation.CalculatorFactory;
using Trip.Insurance.Calculation.Calculators;

namespace Calculator.Tests;

public class InsuranceCalculatorShould
{
    private readonly ICalculatorFactory _calculatorFactory;

    public InsuranceCalculatorShould()
    {
        _calculatorFactory = new CalculatorFactory();
    }

    [Fact]
    public void CalculatePayment()
    {
        var paymentCalculator = _calculatorFactory.CreateCalculator();
        var payment = paymentCalculator.CalculatePayment("Nika");
        Assert.Equal(decimal.Zero, payment);
    }
    
    [Fact]
    public void CalculatePaymentWithCaching()
    {
        var paymentCalculator = _calculatorFactory.CreateCachedCalculator();
        var payment = paymentCalculator.CalculatePayment("Nika");
        Assert.Equal(decimal.Zero, payment);
    }
    
    [Fact]
    public void CalculatePaymentWithLogging()
    {
        var paymentCalculator = _calculatorFactory.CreateLoggingCalculator();
        var payment = paymentCalculator.CalculatePayment("Nika");
        Assert.Equal(decimal.Zero, payment);
    }
    
    [Fact]
    public void CalculatePaymentWithRounding()
    {
        var paymentCalculator = _calculatorFactory.CreateRoundingCalculator();
        var payment = paymentCalculator.CalculatePayment("Nika");
        Assert.Equal(decimal.Zero, payment);
    }
    
    [Fact]
    public void CalculatePaymentWithMultipleDecorators()
    {
        var cachedCalculator = _calculatorFactory.CreateCachedCalculator();
        _calculatorFactory.SetCalculatorDecorator(cachedCalculator, _calculatorFactory.CreateLoggingCalculator());

        cachedCalculator.CalculatePayment("Nika");
    }
}