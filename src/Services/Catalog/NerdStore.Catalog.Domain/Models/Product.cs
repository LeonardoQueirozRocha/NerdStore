using NerdStore.Core.DomainObjects;
using NerdStore.Core.DomainObjects.Exceptions;
using NerdStore.Core.DomainObjects.Interfaces;

namespace NerdStore.Catalog.Domain.Models;

public class Product : Entity, IAggregateRoot
{
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Active { get; private set; }
    public decimal Value { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public string Image { get; private set; }
    public int QuantityInStock { get; private set; }

    public Category Category { get; private set; }

    public Product(
        Guid categoryId,
        string name,
        string description,
        bool active,
        decimal value,
        DateTime registrationDate,
        string image)
    {
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Active = active;
        Value = value;
        RegistrationDate = registrationDate;
        Image = image;

        Validate();
    }

    public void Activate() => Active = true;

    public void Deactivate() => Active = false;

    public void ReplenishStock(int quantity) => QuantityInStock += quantity;

    public bool HasStock(int quantity) => QuantityInStock >= quantity;

    public void DebitStock(int quantity)
    {
        if (quantity < 0) quantity *= -1;
        if (!HasStock(quantity)) throw new DomainException("Estoque insuficiente");
        QuantityInStock -= quantity;
    }

    public void ChangeDescription(string description)
    {
        AssertionConcern.ValidateIfEmpty(description, "O campo Descricao do produto não pode estar vazio");
        Description = description;
    }

    public void ChangeCategory(Category category)
    {
        Category = category;
        CategoryId = category.Id;
    }

    public void Validate()
    {
        AssertionConcern.ValidateIfEmpty(Name, "O campo Nome do produto não pode estar vazio");
        AssertionConcern.ValidateIfEmpty(Description, "O campo Descricao do produto não pode estar vazio");
        AssertionConcern.ValidateIfEqual(CategoryId, Guid.Empty, "O campo CategoriaId do produto não pode estar vazio");
        AssertionConcern.ValidateIfLessThan(Value, 1, "O campo Valor do produto não pode se menor igual a 0");
        AssertionConcern.ValidateIfEmpty(Image, "O campo Imagem do produto não pode estar vazio");
    }
}