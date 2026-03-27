using System.Collections.Generic;

namespace Balatro1
{
    public class BonusCard : Card
    {
        public BonusCard(Suit suit, CardValue value) : base(suit, value) { }

        public override int CalculateBonus(IEnumerable<Card> hand) => 10;

        public override string ToString() => $"[BONUS] {Value} of {Suit} (+10)";
    }
}