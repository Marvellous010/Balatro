using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{
    public class HandEvaluator
    {
        private readonly ICombination _flush = new FlushCombination();
        private readonly ICombination _pair = new PairCombination();

        public ICombination? GetBestCombination(List<Card> cards)
        {
            if (_flush.IsMatch(cards)) return _flush;
            if (_pair.IsMatch(cards)) return _pair;
            return null;
        }

        public bool HasFlush(List<Card> cards) => _flush.IsMatch(cards);
        public bool HasPair(List<Card> cards) => _pair.IsMatch(cards);
    }
}