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

            foreach (var card in CurrentHand)
            {
                Console.WriteLine($"- {card}");
            }

            Console.WriteLine("\nDebug finished. Press any key to exit...");
            Console.ReadKey();
        }
    }
}