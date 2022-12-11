using Mediator.Players.Abstractions;

namespace Mediator.Offers;

public record Offer(string StockName, int NumberOfShares, IPlayer Player);