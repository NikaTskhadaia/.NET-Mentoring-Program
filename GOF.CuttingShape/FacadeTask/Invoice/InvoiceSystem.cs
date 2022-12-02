namespace FacadeTask.Invoice;

public class InvoiceSystem : IInvoiceSystem
{
    public void SendInvoice(Invoice invoice)
    {
        Console.WriteLine("Sending invoice...");
    }
}