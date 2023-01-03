using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.OrderAggregate.ValueObjects;

namespace BugOsPos.Domain.OrderAggregate.Entities;

public sealed class Payment : Entity<PaymentId>
{
    public PaymentType PaymentType { get; }
    public DateTime CreatedAt { get; }
    public List<string> DocumentPaths { get; }

    private Payment(
        PaymentId id,
        PaymentType paymentType,
        DateTime createdAt)
        : base(id)
    {
        PaymentType = paymentType;
        DocumentPaths = new List<string>();
        CreatedAt = createdAt;
    }

    public static Payment New(
        PaymentId id,
        PaymentType paymentType,
        DateTime createdAt)
    {
        return new Payment(id, paymentType, createdAt);
    }
}

public enum PaymentType
{
    Cash,
    Card,
    BankTransfer,
    Other
}
