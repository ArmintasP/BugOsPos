using BugOsPos.Domain.CategoryAggregate.ValueObjects;
using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate.ValueObjects;

namespace BugOsPos.Domain.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId>
{
    public FranchiseId FranchiseId { get; }
    public EmployeeId SpecialistId { get; }
    public DiscountId? DiscountId { get; }
    public CategoryId CategoryId { get; } 
    public string Name { get; }
    public bool IsProduct { get; }
    public decimal Price { get; }
    public decimal PriceBeforeTaxes { get; }
    public decimal Taxes { get; }
    public int Quantity { get; }
    public TimeOnly? Duration { get; }
    public DateTime? ReservationDate { get; }

    private Product(
        ProductId id,
        FranchiseId franchiseId,
        EmployeeId specialistId,
        DiscountId? discountId,
        CategoryId categoryId,
        string name,
        bool isProduct,
        decimal priceBeforeTaxes,
        decimal taxes,
        int quantity,
        TimeOnly? duration = null,
        DateTime? reservationDate = null)
        : base(id)
    {
        FranchiseId = franchiseId;
        SpecialistId = specialistId;
        DiscountId = discountId;
        CategoryId = categoryId;
        Name = name;
        IsProduct = isProduct;
        Price = priceBeforeTaxes * (1 + taxes);
        PriceBeforeTaxes = priceBeforeTaxes;
        Taxes = taxes;
        Quantity = quantity;
        Duration = duration;
        ReservationDate = reservationDate;
    }

    public static Product NewService(
        ProductId id,
        FranchiseId franchiseId,
        EmployeeId specialistId,
        DiscountId? discountId,
        CategoryId categoryId,
        string name,
        decimal priceBeforeTaxes,
        decimal taxes,
        int quantity,
        TimeOnly? duration,
        DateTime? reservationDate)
    {
        return new Product(
            id,
            franchiseId,
            specialistId,
            discountId,
            categoryId,
            name,
            isProduct: false,
            priceBeforeTaxes,
            taxes,
            quantity,
            duration,
            reservationDate);
    }

    public static Product NewProduct(
        ProductId id,
        FranchiseId franchiseId,
        EmployeeId specialistId,
        DiscountId? discountId,
        CategoryId categoryId,
        string name,
        decimal priceBeforeTaxes,
        decimal taxes,
        int quantity)
    {
        return new Product(
            id,
            franchiseId,
            specialistId,
            discountId,
            categoryId,
            name,
            isProduct: true,
            priceBeforeTaxes,
            taxes,
            quantity);
    }
}
