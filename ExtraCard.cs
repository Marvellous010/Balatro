using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{
    public class ExtraCard : Card
    {
        public ExtraCard(Suit suit, CardValue value) : base(suit, value) { }

        public override int CalculateBonus(IEnumerable<Card> hand)
        {
            // Counts how many cards in the hand have the same value (including itself)
            int count = hand.Count(c => c.Value == this.Value);
            return count * 2;
        }

        public override string ToString() => $"[EXTRA] {Value} of {Suit} (+2 per same rank)";
    }
}