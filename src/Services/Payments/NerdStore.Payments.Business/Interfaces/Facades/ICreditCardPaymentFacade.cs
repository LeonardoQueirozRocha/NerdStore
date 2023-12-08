

using NerdStore.Payments.Business.Models;

namespace NerdStore.Payments.Business.Interfaces.Facades;

public interface ICreditCardPaymentFacade
{
    Transaction AccomplishPayment(Order order, Payment payment);
}