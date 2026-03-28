using System;
using System.Collections.Generic;

namespace Balatro1
{
    public class GlassCard : Card
    {
        private static readonly Random _random = new();

        public GlassCard(Suit suit, CardValue value) : base(suit, value) { }

        public override double CalculateMultiplier(IEnumerable<Card> hand) => 2.0;

        public bool ShouldBreak()
        {
            // 1 in 4 chance to break (25%)
            return _random.Next(1, 5) == 1;
        }

        public override string ToString() => $"[GLASS] {Value} of {Suit} (2x Multi)";
    }
}