using Trip.Insurance.Calculation.CalculatorFactory;

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
        var paymentCalculator = _calculatorFactory.CreateCachedInsurancePaymentCalculator();
        var payment = paymentCalculator.CalculatePayment("Nika");
        Assert.Equal(decimal.Zero, payment);
    }
    
    [Fact]
    public void CalculatePaymentWithLogging()
    {
        var paymentCalculator = _calculatorFactory.CreateLoggingInsurancePaymentCalculator();
        var payment = paymentCalculator.CalculatePayment("Nika");
        Assert.Equal(decimal.Zero, payment);
    }
    
    [Fact]
    public void CalculatePaymentWithRounding()
    {
        var paymentCalculator = _calculatorFactory.CreateRoundingInsurancePaymentCalculator();
        var payment = paymentCalculator.CalculatePayment("Nika");
        Assert.Equal(decimal.Zero, payment);
    }
    
    [Fact]
    public void CalculatePaymentWithMultipleDecorators()
    {
        var paymentCalculator = _calculatorFactory.CreateInsurancePaymentCalculatorWithDecorator(
            _calculatorFactory.CreateCachedInsurancePaymentCalculator()
            );
        var payment = paymentCalculator.CalculatePayment("Nika");
        Assert.Equal(decimal.Zero, payment);
    }
}