using Microsoft.Extensions.Logging;
using Trip.Insurance.Calculation.Calculators;

namespace Trip.Insurance.Calculation.CalculatorDecorators;

public class InsurancePaymentCalculatorWithLogging : CalculatorDecorator
{
    private readonly ILogger<InsurancePaymentCalculatorWithLogging> _logger;

    public InsurancePaymentCalculatorWithLogging(ICalculator insurancePaymentCalculator, ILogger<InsurancePaymentCalculatorWithLogging> logger)
        : base(insurancePaymentCalculator)
    {
        _logger = logger;
    }

    public override decimal CalculatePayment(string touristName)
    {
        _logger.LogInformation("Start");
        var payment = base.CalculatePayment(touristName);
        _logger.LogInformation("End");
        return payment;
    }
}