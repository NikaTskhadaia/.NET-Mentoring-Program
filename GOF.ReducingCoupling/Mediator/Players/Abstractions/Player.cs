using Mediator.Offers;

namespace Mediator.Players.Abstractions;

public abstract class Player : IPlayer
{
    protected readonly StockMarket _stockMarket = StockMarket.Instance;
    public Guid Id { get; } = new Guid();
    public int SoldShares { get; set; }
    public int BoughtShares { get; set; }
    
    public Player()
    {
        _stockMarket.DealMade += OnDealMade;
    }

    public void OnDealMade(object sender, DealMadeEventArgs args)
    {
        if (args.SellerId == this.Id)
        {
            SoldShares -= args.NumberOfShares;
            return;
        }

        BoughtShares += args.NumberOfShares;
    }

    public bool SellOffer(string stockName, int numberOfShares)
    {
        var offer = new Offer(stockName, numberOfShares, this);
        return _stockMarket.SendSellOffer(offer);
    }

    public bool BuyOffer(string stockName, int numberOfShares)
    {
        var offer = new Offer(stockName, numberOfShares, this);
        return _stockMarket.SendBuyOffer(offer);
    }

    public void UpdateBoughtShares(Offer offer)
    {
        BoughtShares += offer.NumberOfShares;
    }

    public void UpdateSoldShares(Offer offer)
    {
        SoldShares -= offer.NumberOfShares;
    }
}