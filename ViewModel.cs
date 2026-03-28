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
            Console.WriteLine("balatro debug view");
            int baseChips = 0;
            double totalMulti = 1.0;

            foreach (var card in CurrentHand)
            {
                int chips = (int)card.Value;
                if (chips > 10) chips = 10;
                if (card.Value == CardValue.A) chips = 11;

                baseChips += chips + card.CalculateBonus(CurrentHand);
                totalMulti *= card.CalculateMultiplier(CurrentHand);
                Console.WriteLine($"- {card,-30} | Multiplier: {card.CalculateMultiplier(CurrentHand)}x");
            }

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine($"einde score {baseChips * totalMulti} ({baseChips} Chips * {totalMulti}x)");

            foreach (var card in CurrentHand.ToArray())
            {
                if (card is GlassCard glass && glass.ShouldBreak())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n!!! {card} IS gebroken en verwijderd uit de hand");
                    Console.ResetColor();
                    _model.Hand.Cards.Remove(card);
                }
            }
            Console.WriteLine("\nDruk op elke knop om weg te gaan");
            Console.ReadKey();
        }
    }
}