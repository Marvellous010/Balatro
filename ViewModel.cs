using System;

namespace Balatro1
{
    public class ViewModel
    {
        private GameEngine _engine;

        public ViewModel(Model model)
        {
            _engine = new GameEngine(model);
        }

        public void Run()
        {
            _engine.Reset();
            bool playing = true;

            while (playing)
            {
                Console.Clear();
                Console.WriteLine("Balatro C# Spel");
                Console.WriteLine($"Score: {_engine.TotalScore}");
                Console.WriteLine($"Deck: {_engine.RemainingCards} | Hand: {_engine.CurrentHand.Count}");
                Console.WriteLine("-----------------------------------");

                for (int i = 0; i < _engine.CurrentHand.Count; i++)
                {
                    string check = _engine.SelectedCards.Contains(_engine.CurrentHand[i]) ? "[X]" : "[ ]";
                    Console.Write($"{check} {i + 1}: ");
                    PrintCardWithColor(_engine.CurrentHand[i]);
                    Console.WriteLine();
                }

                Console.WriteLine("-----------------------------------");

                if (_engine.SelectedCards.Count > 0)
                    ShowScore();
                else
                    Console.WriteLine("Kies een kaart");

                Console.WriteLine("\n[1-8] Kies | [S]peel | [D]iscard | [R]eset | [Q]uit");
                string input = Console.ReadLine()?.ToUpper();

                if (string.IsNullOrWhiteSpace(input)) continue;

                if (input == "Q") playing = false;
                else if (input == "S") { _engine.PlayHand(); Console.WriteLine("Gespeeld!"); Console.ReadKey(); }
                else if (input == "D") _engine.DiscardCards();
                else if (input == "R") _engine.Reset();
                else if (int.TryParse(input, out int nr) && nr >= 1 && nr <= _engine.CurrentHand.Count)
                    _engine.ToggleSelection(_engine.CurrentHand[nr - 1]);
            }
        }

        private void PrintCardWithColor(Card card)
        {
            string typePrefix = card.GetTypePrefix();
            string valueStr = card.GetValueString();
            string suitSymbol = card.GetSuitSymbol();

            if (!string.IsNullOrEmpty(typePrefix))
            {
                ConsoleColor typeColor = card.GetType().Name switch
                {
                    "BonusCard" => ConsoleColor.Yellow,
                    "ExtraCard" => ConsoleColor.Cyan,
                    "GlassCard" => ConsoleColor.Magenta,
                    "WildCard" => ConsoleColor.DarkYellow,
                    "SteelCard" => ConsoleColor.Gray,
                    _ => ConsoleColor.White
                };

                Console.ForegroundColor = typeColor;
                Console.Write(typePrefix);
                Console.ResetColor();
            }

            Console.Write(valueStr);

            ConsoleColor suitColor = card.Suit switch
            {
                Suit.Hearts => ConsoleColor.Red,
                
                Suit.Spades => ConsoleColor.Red,
               
                _ => ConsoleColor.White
            };

            Console.ForegroundColor = suitColor;
            Console.Write(suitSymbol);
            Console.ResetColor();
        }

        private void ShowScore()
        {
            var (chips, multi) = _engine.GetCurrentScore();
            var combo = _engine.GetCurrentCombination();

            if (combo != null)
                Console.WriteLine($"Combo: {combo.Name} (+{combo.BaseChips}, x{combo.BaseMultiplier})");

            Console.WriteLine($"Score: {chips * multi}");
        }
    }
}