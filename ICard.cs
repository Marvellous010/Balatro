using System.Collections.Generic;

namespace Balatro1
{
    public interface ICard
    {
        Suit Suit { get; }
        CardValue Value { get; }
        bool IsWild { get; }
        int CalculateBonus(IEnumerable<Card> hand);
        double CalculateMultiplier(IEnumerable<Card> hand);
    }
}