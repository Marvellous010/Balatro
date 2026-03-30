using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{
    public class FlushCombination : ICombination
    {
        public string Name => "Flush";
        public int BaseChips => 35;
        public double BaseMultiplier => 4.0;

        public bool IsMatch(IEnumerable<Card> hand)
        {
            if (hand.Count() < 5) return false;
            var suitGroups = hand.Where(c => !c.IsWild).GroupBy(c => c.Suit);
            int wildCount = hand.Count(c => c.IsWild);
            return suitGroups.Any(g => g.Count() + wildCount >= 5);
        }
    }
}