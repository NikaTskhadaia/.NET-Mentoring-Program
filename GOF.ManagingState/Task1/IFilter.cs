namespace Task1;

public interface IFilter
{
    IEnumerable<Trade> Match(IEnumerable<Trade> trades);
}