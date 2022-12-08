using Trip.Insurance.Calculation.Calculators;

namespace Trip.Insurance.Calculation.CalculatorFactory;

public interface ICalculatorFactory
{
    ICalculator CreateCalculator();
    ICalculator CreateCachedInsurancePaymentCalculator();
    ICalculator CreateLoggingInsurancePaymentCalculator();
    ICalculator CreateRoundingInsurancePaymentCalculator();
    ICalculator CreateInsurancePaymentCalculatorWithDecorator(ICalculator calculator);

}