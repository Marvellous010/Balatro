using System;
using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{
    public class ViewModel
    {
        private Model _model;
        public List<Card> CurrentHand { get; private set; } = new();

        public ViewModel(Model model) => _model = model;

        public void UpdateFromModel() => CurrentHand = new List<Card>(_model.Hand.Cards);

        public void Run()
        {
            bool playing = true;
            DealNewHand();

            while (playing)
            {
                Console.Clear();
                UpdateFromModel();

                Console.WriteLine("=== BALATRO GAME LOOP ===");
                Console.WriteLine($"Kaarten over in deck: {_model.Deck.RemainingCards}");
                Console.WriteLine("---------------------------------------------------------");

                int baseChips = 0;
                double totalMulti = 1.0;

                foreach (var card in CurrentHand)
                {
                    int chips = (int)card.Value;
                    if (chips > 10) chips = 10;
                    if (card.Value == CardValue.A) chips = 11;

                    baseChips += chips + card.CalculateBonus(CurrentHand);
                    totalMulti *= card.CalculateMultiplier(CurrentHand);

                    string specialInfo = card.IsWild ? " [WILD]" : "";
                    Console.WriteLine($"- {card,-30}{specialInfo} | Multi: {card.CalculateMultiplier(CurrentHand)}x");
                }

                if (CheckForFlush())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n>>> FLUSH GEDETECTEERD! (+35 Chips, 4x Mult) <<<");
                    Console.ResetColor();
                    baseChips += 35;
                    totalMulti *= 4;
                }

                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine($"Hand Score: {baseChips * totalMulti} ({baseChips} Chips * {totalMulti}x Multi)");
                Console.WriteLine("---------------------------------------------------------");

                Console.WriteLine("\n[S]peel Hand (Check Breuk) | [N]ieuwe Hand | [Q]uit");
                string input = Console.ReadLine()?.ToUpper();

                if (input == "S")
                {
                    CheckGlassBreaking();
                    Console.WriteLine("\nDruk op een toets om door te gaan...");
                    Console.ReadKey();
                }
                else if (input == "N") DealNewHand();
                else if (input == "Q") playing = false;
            }
        }

        private void DealNewHand()
        {
            _model.Deck.Reset();
            _model.Deck.Shuffle();
            _model.Hand.Cards.Clear();
            for (int i = 0; i < _model.Hand.MaxCards; i++)
            {
                var card = _model.Deck.TakeCard();
                if (card != null) _model.Hand.AddCard(card);
            }
        }

        private void CheckGlassBreaking()
        {
            foreach (var card in CurrentHand.ToArray())
            {
                if (card is GlassCard glass && glass.ShouldBreak())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n!!! {card} IS GEBROKEN en uit het spel verwijderd!");
                    Console.ResetColor();
                    _model.Hand.Cards.Remove(card);
                }
            }
        }

        private bool CheckForFlush()
        {
            if (CurrentHand.Count < 5) return false;
            var suitGroups = CurrentHand.Where(c => !c.IsWild).GroupBy(c => c.Suit);
            int wildCount = CurrentHand.Count(c => c.IsWild);
            return suitGroups.Any(g => g.Count() + wildCount >= 5);
        }
    }
}