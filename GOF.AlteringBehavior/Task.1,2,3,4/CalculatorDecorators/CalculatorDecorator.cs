using Trip.Insurance.Calculation.Calculators;

namespace Trip.Insurance.Calculation.CalculatorDecorators;

public abstract class CalculatorDecorator : ICalculator
{
    protected readonly ICalculator _calculator;

    protected CalculatorDecorator(ICalculator calculator)
    {
        _calculator = calculator;
    }

    public virtual decimal CalculatePayment(string touristName)
    {
        return _calculator.CalculatePayment(touristName);
    }
}