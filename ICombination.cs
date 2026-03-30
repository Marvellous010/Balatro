using System.Collections.Generic;

namespace Balatro1
{
    public interface ICombination
    {
        string Name { get; }
        int BaseChips { get; }
        double BaseMultiplier { get; }
        bool IsMatch(IEnumerable<Card> hand);
    }
}