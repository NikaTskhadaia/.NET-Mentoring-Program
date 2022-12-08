namespace Shipment.Calculation.Strategies;

public class UspsStrategy : IShipmentStrategy
{
    public double Calculate(Order order)
    {
        return order.Product == ProductType.Book ? 3.00d * 0.9 : 3.00d;
    }
}