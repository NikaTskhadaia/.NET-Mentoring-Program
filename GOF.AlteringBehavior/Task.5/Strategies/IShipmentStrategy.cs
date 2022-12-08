namespace Shipment.Calculation.Strategies;

public interface IShipmentStrategy
{
    double Calculate(Order order);
}