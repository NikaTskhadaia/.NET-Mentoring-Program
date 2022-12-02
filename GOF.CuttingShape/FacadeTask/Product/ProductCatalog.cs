namespace FacadeTask.Product;

public class ProductCatalog : IProductCatalog
{
    private IEnumerable<Product> _products = Array.Empty<Product>();
    
    public Product GetProductDetails(string productId)
    {
        Console.WriteLine("Retrieving product...");
        return _products.FirstOrDefault(p => p.Id == productId);
    }
}