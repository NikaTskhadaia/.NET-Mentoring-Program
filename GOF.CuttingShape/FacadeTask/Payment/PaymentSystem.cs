namespace FacadeTask.Payment;

public class PaymentSystem : IPaymentSystem
{
    public bool MakePayment(Payment payment)
    {
        Console.WriteLine("Making payment...");
        return true;
    }
}