using Shipment.Calculation.Strategies;

namespace Shipment.Calculation;

public class ShipmentCalculator
{
    private readonly IShipmentStrategy _shipmentStrategy;

    public ShipmentCalculator(IShipmentStrategy shipmentStrategy)
    {
        _shipmentStrategy = shipmentStrategy;
    }

    public double CalculatePrice(Order order)
    {
        return _shipmentStrategy.Calculate(order);
    }
}