using Trip.Insurance.Calculation.Calculators;

namespace Trip.Insurance.Calculation.CalculatorDecorators;

public class InsurancePaymentCalculatorWithDecorators : CalculatorDecorator
{
    public InsurancePaymentCalculatorWithDecorators(ICalculator paymentCalculator) : base(paymentCalculator)
    {
    }
    
    public override decimal CalculatePayment(string touristName)
    {
        return base.CalculatePayment(touristName);
    }
}