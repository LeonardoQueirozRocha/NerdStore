using NerdStore.Core.Data;
using NerdStore.Payments.Business.Models;

namespace NerdStore.Payments.Business.Interfaces.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
    void AddPayment(Payment payment);
    void AddTransaction(Transaction transaction);
}