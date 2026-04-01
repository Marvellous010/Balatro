using System.Collections.Generic;

namespace Balatro1
{
    public class WildCard : Card
    {
        public WildCard(Suit suit, CardValue value) : base(suit, value) { }

        public override bool IsWild => true;
        public override int CalculateBonus(IEnumerable<Card> hand) => 0;
        public override double CalculateMultiplier(IEnumerable<Card> hand) => 1.0;

        public override string ToString() => $"[W] {Value} of {Suit}";
    }
}