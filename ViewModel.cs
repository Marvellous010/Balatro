using System;
using System.Collections.Generic;

namespace Balatro1
{
    public class ViewModel
    {
        private Model _model;
        public List<Card> CurrentHand { get; private set; } = new();

        public ViewModel(Model model)
        {
            _model = model;
        }

        public void UpdateFromModel()
        {
            CurrentHand = new List<Card>(_model.Hand.Cards);
        }

        public void Run()
        {
            Console.WriteLine("--- Balatro Debug View ---");
            Console.WriteLine($"Cards in hand: {CurrentHand.Count}");

            int totalChips = 0;
            int totalBonus = 0;

            foreach (var card in CurrentHand)
            {
                // Basic Balatro scoring (2-10 = value, Face/Ace = 10 or 11)
                int chips = (int)card.Value;
                if (chips > 10) chips = 10;
                if (card.Value == CardValue.A) chips = 11;

                int bonus = card.CalculateBonus(CurrentHand);

                totalChips += chips;
                totalBonus += bonus;

                Console.WriteLine($"- {card,-30} | Chips: {chips,2} | Bonus: {bonus,2}");
            }

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine($"Total: ({totalChips} Chips + {totalBonus} Bonus) = {totalChips + totalBonus} points");
            Console.WriteLine("\nDebug finished. Press any key to exit...");
            Console.ReadKey();
        }
    }
}