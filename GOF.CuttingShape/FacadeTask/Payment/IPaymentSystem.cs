namespace FacadeTask.Payment;

public interface IPaymentSystem
{
    bool MakePayment(Payment payment); 
}