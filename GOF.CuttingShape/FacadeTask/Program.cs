using FacadeTask;
using FacadeTask.Invoice;
using FacadeTask.Payment;
using FacadeTask.Product;

var productCatalog = new ProductCatalog();
var invoiceSystem = new InvoiceSystem();
var paymentSystem = new PaymentSystem();

var orderFacade = new OrderFacade(productCatalog, invoiceSystem, paymentSystem);
orderFacade.PlaceOrder(string.Empty, int.MinValue, string.Empty);

Console.WriteLine("Order placed");