public class ElementsAdapter<T> : IContainer<T>
{
    private readonly IElements<T> _elements;

    public ElementsAdapter(IElements<T> elements)
    {
        _elements = elements;
    }

    public IEnumerable<T> Items => _elements.GetElements();

    public int Count { get; }
}