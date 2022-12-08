using Trip.Insurance.Calculation.CalculatorDecorators;
using Trip.Insurance.Calculation.Calculators;

namespace Trip.Insurance.Calculation.CalculatorFactory;

public interface ICalculatorFactory
{
    ICalculator CreateCalculator();
    ICalculator CreateCachedCalculator();
    ICalculator CreateLoggingCalculator();
    ICalculator CreateRoundingCalculator();
    void SetCalculatorDecorator(ICalculator decorator, ICalculator calculator);

}