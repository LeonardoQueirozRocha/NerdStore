using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Sales.Domain.Interfaces.Repositories;
using NerdStore.Sales.Domain.Models;
using NerdStore.Sales.Domain.Models.Enums;

namespace NerdStore.Sales.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SalesContext _context;

    public OrderRepository(SalesContext context) => 
        _context = context;

    public IUnitOfWork UnitOfWork => 
        _context;

    public async Task<Order> GetByIdAsync(Guid id) => 
        await _context.Orders.FindAsync(id);

    public async Task<IEnumerable<Order>> GetListByCustomerIdAsync(Guid customerId) => 
        await _context.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();

    public async Task<Order> GetOrderDraftByCustomerIdAsync(Guid customerId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.OrderStatus == OrderStatus.Draft);

        if (order is null) return null;

        await _context
            .Entry(order)
            .Collection(i => i.OrderItems)
            .LoadAsync();

        if (order.VoucherId is not null)
            await _context
                .Entry(order)
                .Reference(i => i.Voucher)
                .LoadAsync();

        return order;
    }

    public void Add(Order order) => 
        _context.Orders.Add(order);

    public void Update(Order order) => 
        _context.Orders.Update(order);

    public async Task<OrderItem> GetItemByIdAsync(Guid id) => 
        await _context.OrderItems.FindAsync(id);

    public async Task<OrderItem> GetItemByOrderAsync(Guid orderId, Guid productId) => 
        await _context.OrderItems
            .FirstOrDefaultAsync(o => o.ProductId == productId && o.OrderId == orderId);

    public void AddItem(OrderItem orderItem) => 
        _context.OrderItems.Add(orderItem);

    public void UpdateItem(OrderItem orderItem) => 
        _context.OrderItems.Update(orderItem);

    public void RemoveItem(OrderItem orderItem) => 
        _context.OrderItems.Remove(orderItem);

    public async Task<Voucher> GetVoucherByCodeAsync(string code) => 
        await _context.Vouchers.FirstOrDefaultAsync(o => o.Code == code);

    public void Dispose() => 
        _context?.Dispose();
}