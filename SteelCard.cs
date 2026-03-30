using System.Collections.Generic;

namespace Balatro1
{
    public class SteelCard : Card
    {
        public SteelCard(Suit suit, CardValue value) : base(suit, value) { }

        // Als de SteelCard gespeeld wordt, doet hij niets extra's
        public override int CalculateBonus(IEnumerable<Card> hand) => 0;
        public override double CalculateMultiplier(IEnumerable<Card> hand) => 1.0;

        // De passieve bonus voor als hij in de hand blijft
        public double PassiveMultiplier => 1.5;

        public override string ToString() => $"[STEEL] {Value} of {Suit} (1.5x in hand)";
    }
}