using System.Collections.Generic;

namespace Balatro1
{
    public class Card
    {
        public Suit Suit { get; }
        public CardValue Value { get; }
        public virtual bool IsWild => false;

        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public virtual int CalculateBonus(IEnumerable<Card> hand) => 0;
        public virtual double CalculateMultiplier(IEnumerable<Card> hand) => 1.0;

        public override string ToString() => $"{(IsWild ? "[WILD] " : "")}{Value} of {Suit}";
    }
}