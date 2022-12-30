using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.OrderAggregate.ValueObjects;

namespace BugOsPos.Domain.OrderAggregate.Entities;

public sealed class Payment : Entity<PaymentId>
{
    public PaymentType PaymentType { get; }
    public List<string> DocumentPaths { get; }

    private Payment(
        PaymentId id,
        PaymentType paymentType)
        : base(id)
    {
        PaymentType = paymentType;
        DocumentPaths = new List<string>();
    }

    public static Payment New(
        PaymentId id,
        PaymentType paymentType)
    {
        return new Payment(id, paymentType);
    }
}

public enum PaymentType
{
    Cash,
    Card,
    BankTransfer,
    Other
}
