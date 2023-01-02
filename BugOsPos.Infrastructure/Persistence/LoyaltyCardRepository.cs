using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Application.LoyaltyCards;
using BugOsPos.Domain.LoyaltyCardAggregate;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class LoyaltyCardRepository : ILoyaltyCardRepository
{
    private static readonly List<LoyaltyCard> _loyaltyCards = PrefilledData.SampleLoyaltyCards();
    private int _nextId = _loyaltyCards.Count + 1;

    public LoyaltyCardId NextIdentity()
    {
        return LoyaltyCardId.New(_nextId);
    }

    public Task Add(LoyaltyCard loyaltyCard)
    {
        _loyaltyCards.Add(loyaltyCard);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<LoyaltyCard?> GetLoyaltyCardById(int id)
    {
        var LoyaltyCard = _loyaltyCards.SingleOrDefault(
            e => e.Id.Value == id);

        return Task.FromResult(LoyaltyCard);
    }

    public Task Update(LoyaltyCard loyaltyCard)
    {
        var index = _loyaltyCards.FindIndex(p => p.Id == loyaltyCard.Id);
        _loyaltyCards[index] = loyaltyCard;
        return Task.CompletedTask;
    }
}
