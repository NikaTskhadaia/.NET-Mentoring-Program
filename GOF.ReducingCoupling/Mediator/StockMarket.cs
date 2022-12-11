using Mediator.Offers;
using Mediator.Players.Abstractions;

namespace Mediator;

public class StockMarket
{
    private static readonly List<Offer> _sellOffers;
    private static readonly List<Offer> _buyOffers;
    
    static StockMarket()
    {
        _sellOffers = new(0);
        _buyOffers = new(0);
    }
    
    public static StockMarket Instance { get; } = new();

    public event EventHandler<DealMadeEventArgs> DealMade;
    
    public void NotifyPlayers(Offer sellOffer, Offer buyOffer)
    {
        sellOffer.Player.UpdateSoldShares(sellOffer);
        buyOffer.Player.UpdateBoughtShares(buyOffer);
    }

    public bool SendSellOffer(Offer sellOffer)
    {
        var buyOffer = _buyOffers.FirstOrDefault(o => o.Player.Id != sellOffer.Player.Id && 
                                                   o.StockName == sellOffer.StockName && 
                                                   o.NumberOfShares == sellOffer.NumberOfShares);
        if (buyOffer is not null)
        {
            Console.WriteLine("Deal is made...");
            _buyOffers.Remove(buyOffer);
            NotifyPlayers(sellOffer, buyOffer);
            /*var dealMadeArgs = new DealMadeEventArgs
            {
                NumberOfShares = sellOffer.NumberOfShares,
                SellerId = sellOffer.Player.Id,
                BuyerId = buyOffer.Player.Id
            };
            DealMade(this, dealMadeArgs);*/
            return true;
        }
        
        _sellOffers.Add(sellOffer);
        return false;
    }
    
    public bool SendBuyOffer(Offer buyOffer)
    {
        var sellOffer = _sellOffers.FirstOrDefault(o => o.Player.Id != buyOffer.Player.Id && 
                                                      o.StockName == buyOffer.StockName && 
                                                      o.NumberOfShares == buyOffer.NumberOfShares);
        if (sellOffer is not null)
        {
            Console.WriteLine("Deal is made...");
            _sellOffers.Remove(sellOffer);
            NotifyPlayers(sellOffer, buyOffer);
            /*var dealMadeArgs = new DealMadeEventArgs
            {
                NumberOfShares = sellOffer.NumberOfShares,
                SellerId = sellOffer.Player.Id,
                BuyerId = buyOffer.Player.Id
            };
            DealMade(this, dealMadeArgs);*/
            return true;
        }
        
        _buyOffers.Add(buyOffer);
        return false;
    }
}