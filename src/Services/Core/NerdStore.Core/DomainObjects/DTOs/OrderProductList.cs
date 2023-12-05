namespace NerdStore.Core.DomainObjects.DTOs;

public class OrderProductsList
{
    public Guid OrderId { get; set; }
    public ICollection<Item> Items { get; set; }
}