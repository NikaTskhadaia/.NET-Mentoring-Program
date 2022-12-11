using Mediator.Players.Abstractions;

namespace Mediator;

public class PlayerComparer : IEqualityComparer<IPlayer>
{
    public bool Equals(IPlayer? x, IPlayer? y)
    {
        if (x == null && y == null)
            return true;
        else if (x == null || y == null)
            return false;
        
        return x.Id.Equals(y.Id) || false;
    }

    public int GetHashCode(IPlayer obj)
    {
        return obj.GetHashCode();
    }
}