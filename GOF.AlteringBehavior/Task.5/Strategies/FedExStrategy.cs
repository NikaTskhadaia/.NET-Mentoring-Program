namespace Shipment.Calculation.Strategies;

public class FedExStrategy : IShipmentStrategy
{
    public double Calculate(Order order)
    {
        return order.Weight > 300 ? 5.00d * 1.1 : 5.00d;
    }
}