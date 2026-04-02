using System;
using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{
    public class ScoreCalculator
    {
        private readonly HandEvaluator _evaluator = new HandEvaluator();

        public (int chips, double multiplier) Calculate(List<Card> selectedCards, List<Card> allCards)
        {
            int chips = 0;
            double multi = 1.0;

            var combo = _evaluator.GetBestCombination(selectedCards);
            if (combo != null)
            {
                chips += combo.BaseChips;
                multi = combo.BaseMultiplier;
            }

            foreach (var card in selectedCards)
            {
                int val = Math.Min((int)card.Value, 10);
                if (card.Value == CardValue.A) val = 11;
                chips += val + card.CalculateBonus(selectedCards);
                multi *= card.CalculateMultiplier(selectedCards);
            }

            foreach (var card in allCards.Except(selectedCards))
            {
                if (card is SteelCard s)
                    multi *= s.PassiveMultiplier;
            }

            return (chips, multi);
        }
    }
}