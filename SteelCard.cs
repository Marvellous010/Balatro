using System.Collections.Generic;

namespace Balatro1
{
    public class SteelCard : Card
    {
        public SteelCard(Suit suit, CardValue value) : base(suit, value) { }

        public override int CalculateBonus(IEnumerable<Card> hand) => 0;
        public override double CalculateMultiplier(IEnumerable<Card> hand) => 1.0;

        public double PassiveMultiplier => 1.5;
    }
}