using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{
    public class PairCombination : ICombination
    {
        public string Name => "Pair";
        public int BaseChips => 10;
        public double BaseMultiplier => 2.0;

        public bool IsMatch(IEnumerable<Card> hand)
        {
            int wildCount = hand.Count(c => c.IsWild);
            var groups = hand.Where(c => !c.IsWild).GroupBy(c => c.Value);

            if (groups.Any(g => g.Count() >= 2)) return true;
            if (wildCount >= 1 && hand.Count(c => !c.IsWild) >= 1) return true;
            if (wildCount >= 2) return true;

            return false;
        }
    }
}