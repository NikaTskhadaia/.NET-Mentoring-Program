namespace FacadeTask.Invoice;

public interface IInvoiceSystem
{
    void SendInvoice(Invoice invoice);
}