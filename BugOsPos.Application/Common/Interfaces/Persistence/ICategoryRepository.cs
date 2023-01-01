using BugOsPos.Domain.CategoryAggregate;
using BugOsPos.Domain.CategoryAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
    CategoryId NextIdentity();
    Task<Category?> GetCategoryById(CategoryId id);
    Task Add(Category category);
}
