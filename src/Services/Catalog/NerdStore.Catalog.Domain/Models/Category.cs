using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Models;

public class Category : Entity
{
    public string Name { get; private set; }
    public int Code { get; private set; }

    public Category(string name, int code)
    {
        Name = name;
        Code = code;

        Validate();
    }

    public override string ToString() => $"{Name} - {Code}";

    public void Validate()
    {
        AssertionConcern.ValidateIfEmpty(Name, "O campo Nome da categoria não pode estar vazio");
        AssertionConcern.ValidateIfEqual(Code, 0, "O campo Codigo não pode ser 0");
    }
}