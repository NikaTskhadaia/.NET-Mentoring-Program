public class Printer
{
    public void Print<T>(IContainer<T> container)
    {
        foreach (var item in container.Items)
        {
            Console.WriteLine(item?.ToString());
        }
    }
}
