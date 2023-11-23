using NerdStore.Core.DomainObjects;
using NerdStore.Core.DomainObjects.Exceptions;
using NerdStore.Core.DomainObjects.Interfaces;
using NerdStore.Sales.Domain.Models.Enums;

namespace NerdStore.Sales.Domain.Models;

public class Order : Entity, IAggregateRoot
{
    private readonly List<OrderItem> _orderItems;

    public int Code { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUsed { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalValue { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public virtual Voucher Voucher { get; set; }

    public Order(
        Guid customerId,
        bool voucherUsed,
        decimal discount,
        decimal totalValue)
    {
        CustomerId = customerId;
        VoucherUsed = voucherUsed;
        Discount = discount;
        TotalValue = totalValue;
        _orderItems = new List<OrderItem>();
    }

    protected Order()
    {
        _orderItems = new List<OrderItem>();
    }

    public void CalculateOrderValue()
    {
        TotalValue = OrderItems.Sum(p => p.CalculateValue());
        CalculateDiscountTotalValue();
    }

    public void CalculateDiscountTotalValue()
    {
        if (!VoucherUsed) return;

        decimal discount = 0;
        var value = TotalValue;

        if (Voucher.VoucherDiscountType == VoucherDiscountType.Percentage &&
            Voucher.Percentage.HasValue)
        {
            discount = value * Voucher.Percentage.Value / 100;
            value -= discount;
        }
        else
        {
            if (Voucher.DiscountValue.HasValue)
            {
                discount = Voucher.DiscountValue.Value;
                value -= discount;
            }
        }

        TotalValue = value < 0 ? 0 : value;
        Discount = discount;
    }

    public bool ExistOrderItem(OrderItem item)
    {
        return _orderItems.Any(p => p.ProductId == item.ProductId);
    }

    public void AddItem(OrderItem item)
    {
        if (!item.IsValid()) return;

        item.AssociateOrder(Id);

        if (ExistOrderItem(item))
        {
            var existingItem = GetItemByProductId(item.ProductId);
            existingItem.AddUnits(item.Quantity);
            item = existingItem;
            _orderItems.Remove(existingItem);
        }

        item.CalculateValue();
        _orderItems.Add(item);

        CalculateOrderValue();
    }

    public void RemoveItem(OrderItem item)
    {
        if (!item.IsValid()) return;

        var existingItem = GetItemByProductId(item.ProductId);

        _orderItems.Remove(existingItem);

        CalculateOrderValue();
    }

    public void UpdateItem(OrderItem item)
    {
        if (!item.IsValid()) return;

        item.AssociateOrder(Id);

        var existingItem = GetItemByProductId(item.ProductId);

        _orderItems.Remove(existingItem);
        _orderItems.Add(item);

        CalculateOrderValue();
    }

    public void UpdateUnits(OrderItem item, int units)
    {
        item.UpdateUnits(units);
        UpdateItem(item);
    }

    public void MakeDraft()
    {
        OrderStatus = OrderStatus.Draft;
    }

    public void StartOrder()
    {
        OrderStatus = OrderStatus.Started;
    }

    public void FinalizeOrder()
    {
        OrderStatus = OrderStatus.Paid;
    }

    public void CancelOrder()
    {
        OrderStatus = OrderStatus.Canceled;
    }

    private OrderItem GetItemByProductId(Guid productId) =>
        OrderItems.FirstOrDefault(p => p.ProductId == productId) ??
            throw new DomainException("O item não pertence ao pedido");

    public static class OrderFactory
    {
        public static Order NewOrderDraft(Guid customerId)
        {
            var order = new Order { CustomerId = customerId };

            order.MakeDraft();

            return order;
        }
    }
}