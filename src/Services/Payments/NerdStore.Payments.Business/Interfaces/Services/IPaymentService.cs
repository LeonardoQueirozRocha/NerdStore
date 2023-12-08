using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Payments.Business.Models;

namespace NerdStore.Payments.Business.Interfaces.Services;

public interface IPaymentService
{
    Task<Transaction> AccomplishOrderPaymentAsync(OrderPayment orderPayment);
}