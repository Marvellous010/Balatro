using System.Collections.Generic;

namespace Balatro1
{
    public class Card
    {
        public Suit Suit { get; }
        public CardValue Value { get; }

        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public virtual int CalculateBonus(IEnumerable<Card> hand) => 0;

        // Default multiplier is 1.0x
        public virtual double CalculateMultiplier(IEnumerable<Card> hand) => 1.0;

        public override string ToString() => $"{Value} of {Suit}";
    }
}