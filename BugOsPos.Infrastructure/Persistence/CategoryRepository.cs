using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CategoryAggregate;
using BugOsPos.Domain.CategoryAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class CategoryRepository : ICategoryRepository
{
    private static readonly List<Category> _categories = PrefilledData.SampleCategories();
    private int _nextId = _categories.Count + 1;

    public CategoryId NextIdentity()
    {
        return CategoryId.New(_nextId);
    }

    public Task Add(Category category)
    {
        _categories.Add(category);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<Category?> GetCategoryById(CategoryId id)
    {
        var Category = _categories.SingleOrDefault(
            e => e.Id == id);

        return Task.FromResult(Category);
    }

    public Task Update(Category category)
    {
        var index = _categories.FindIndex(p => p.Id == category.Id);
        _categories[index] = category;
        return Task.CompletedTask;
    }
}
