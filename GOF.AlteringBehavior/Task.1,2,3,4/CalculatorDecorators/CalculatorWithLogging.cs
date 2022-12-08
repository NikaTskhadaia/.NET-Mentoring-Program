using Microsoft.Extensions.Logging;
using Trip.Insurance.Calculation.Calculators;

namespace Trip.Insurance.Calculation.CalculatorDecorators;

public class CalculatorWithLogging : CalculatorDecorator
{
    private readonly ILogger<CalculatorWithLogging> _logger;

    public CalculatorWithLogging(ICalculator insurancePaymentCalculator, ILogger<CalculatorWithLogging> logger)
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