using System;
using System.Collections.Generic;

namespace Balatro1
{
    public class GlassCard : Card
    { //static functie met chatgpt hulp
        private static readonly Random _random = new();

        public GlassCard(Suit suit, CardValue value) : base(suit, value) { }

        public override int CalculateBonus(IEnumerable<Card> hand) => 0;
        public override double CalculateMultiplier(IEnumerable<Card> hand) => 2.0;

        public bool ShouldBreak() => _random.Next(1,5) == 1;
    }
}