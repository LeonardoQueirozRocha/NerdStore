using NerdStore.Core.Data;
using NerdStore.Payments.Business.Interfaces.Repositories;
using NerdStore.Payments.Business.Models;

namespace NerdStore.Payments.Data.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentContext _context;

    public PaymentRepository(PaymentContext context) => 
        _context = context;

    public IUnitOfWork UnitOfWork => 
        _context;

    public void AddPayment(Payment payment) => 
        _context.Payments.Add(payment);

    public void AddTransaction(Transaction transaction) => 
        _context.Transactions.Add(transaction);

    public void Dispose() => 
        _context.Dispose();
}