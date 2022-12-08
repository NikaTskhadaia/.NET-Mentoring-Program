namespace Shipment.Calculation.Strategies;

public class UpsStrategy : IShipmentStrategy
{
    public double Calculate(Order order)
    {
        return order.Weight > 400 ? 4.25d * 1.1 : 4.25d;
    }
}