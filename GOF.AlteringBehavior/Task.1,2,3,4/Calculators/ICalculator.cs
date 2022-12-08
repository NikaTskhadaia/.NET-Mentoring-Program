namespace Trip.Insurance.Calculation.Calculators;

public interface ICalculator
{
    decimal CalculatePayment(string touristName);
}