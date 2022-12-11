using Mediator.Offers;

namespace Mediator;

public class DealMadeEventArgs : EventArgs
{
    public int NumberOfShares { get; set; }
    public Guid SellerId { get; set; }
    public Guid BuyerId { get; set; }
}