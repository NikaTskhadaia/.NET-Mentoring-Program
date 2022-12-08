using System;
using Trip.Insurance.Calculation.Calculators;

namespace Trip.Insurance.Calculation.CalculatorDecorators;

public class InsurancePaymentCalculatorWithRounding : CalculatorDecorator
{
    public InsurancePaymentCalculatorWithRounding(ICalculator insurancePaymentCalculator) : base(insurancePaymentCalculator)
    {
    }

    public override decimal CalculatePayment(string touristName)
    {
        var payment = base.CalculatePayment(touristName);
        return Math.Round(payment, MidpointRounding.ToEven);
    }
}