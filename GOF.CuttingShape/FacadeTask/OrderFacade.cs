using FacadeTask.Invoice;
using FacadeTask.Payment;
using FacadeTask.Product;

namespace FacadeTask;

public class OrderFacade
{
    private readonly IProductCatalog _productCatalog;
    private readonly IInvoiceSystem _invoiceSystem;
    private readonly IPaymentSystem _paymentSystem;

    public OrderFacade(IProductCatalog productCatalog, IInvoiceSystem invoiceSystem, IPaymentSystem paymentSystem)
    {
        _productCatalog = productCatalog;
        _invoiceSystem = invoiceSystem;
        _paymentSystem = paymentSystem;
    }

    public void PlaceOrder(string productId, int quantity, string email)
    {
        _productCatalog.GetProductDetails(productId);
        _paymentSystem.MakePayment(new Payment.Payment());
        _invoiceSystem.SendInvoice(new Invoice.Invoice());
    }
}