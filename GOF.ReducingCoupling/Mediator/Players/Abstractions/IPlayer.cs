using Mediator.Offers;

namespace Mediator.Players.Abstractions;

public interface IPlayer
{
    public Guid Id { get; }
    int SoldShares { get; }
    int BoughtShares { get; }

    bool SellOffer(string stockName, int numberOfShares);
    bool BuyOffer(string stockName, int numberOfShares);
    void UpdateBoughtShares(Offer offer);
    void UpdateSoldShares(Offer offer);
}