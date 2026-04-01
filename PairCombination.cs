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
            return hand.GroupBy(c => c.Value).Any(g => g.Count() >= 2);
        }
    }
}