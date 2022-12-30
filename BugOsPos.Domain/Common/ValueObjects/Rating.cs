using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.Common.ValueObjects;

public sealed class Rating : ValueObject
{
    public decimal Value { get; private set;  }
    public int NumberOfRatings { get; private set; }

    private Rating(decimal value, int numberOfRatings)
    {
        Value = value;
        NumberOfRatings = numberOfRatings;
    }

    public static Rating New(decimal value, int numberOfRatings) => new Rating(value, numberOfRatings);
    public static Rating New() => new Rating(0, 0);

    public void AddNewRating(decimal value)
    {
        Value = (Value * NumberOfRatings + value) / (NumberOfRatings + 1);
        NumberOfRatings++;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
