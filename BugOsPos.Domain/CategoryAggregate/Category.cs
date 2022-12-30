using BugOsPos.Domain.CategoryAggregate.ValueObjects;
using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;

namespace BugOsPos.Domain.CategoryAggregate;

public sealed class Category : AggregateRoot<CategoryId>
{
    public string Name { get; }
    public string Description { get; }
    public FranchiseId FranchiseId { get; }

    private Category(CategoryId id, string name, string description, FranchiseId franchiseId) : base(id)
    {
        Name = name;
        Description = description;
        FranchiseId = franchiseId;
    }

    public static Category New(CategoryId id, string name, string description, FranchiseId franchiseId)
    {
        return new Category(id, name, description, franchiseId);
    }
}
