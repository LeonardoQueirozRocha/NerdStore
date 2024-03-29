using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain.Models;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitValue { get; private set; }

    // EF Relation
    public Order Order { get; set; }

    public OrderItem() { }

    public OrderItem(
        Guid productId,
        string productName,
        int quantity,
        decimal unitValue)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitValue = unitValue;
    }

    public decimal CalculateValue() => Quantity * UnitValue;

    public override bool IsValid() => true;

    internal void AssociateOrder(Guid orderId) => OrderId = orderId;

    internal void AddUnits(int units) => Quantity += units;

    internal void UpdateUnits(int units) => Quantity = units;
}
