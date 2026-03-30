using System;
using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{
    public class ViewModel
    {
        private Model _model;
        private List<Card> _selectedCards = new();
        private double _totalScore = 0;
        private readonly ICombination _flush = new FlushCombination();

        public List<Card> CurrentHand => _model.Hand.Cards;

        public ViewModel(Model model) => _model = model;

        public void Run()
        {
            ResetGame();
            bool playing = true;

            while (playing)
            {
                Console.Clear();
                Console.WriteLine("Balatro C# Spel");
                Console.WriteLine($"Totaal Score: {_totalScore}");
                Console.WriteLine($"Deck: {_model.Deck.RemainingCards} | Hand: {CurrentHand.Count}");
                Console.WriteLine("---------------------------------------------------------");

                // Weer gewoon onder elkaar (verticaal)
                for (int i = 0; i < CurrentHand.Count; i++)
                {
                    string prefix = _selectedCards.Contains(CurrentHand[i]) ? "[X]" : "[ ]";
                    Console.WriteLine($"{prefix} {i + 1}: {CurrentHand[i]}");
                }

                Console.WriteLine("---------------------------------------------------------");

                if (_selectedCards.Count > 0) ShowLiveScore();
                else Console.WriteLine("Selecteer een kaart");

                Console.WriteLine("\n[1-8] Kies | S[P]eel | [D]iscard | [R]eset | [Q]uit");
                string input = Console.ReadLine()?.ToUpper();

                if (string.IsNullOrWhiteSpace(input)) continue;

                if (input == "Q") playing = false;
                else if (input == "P") PlayHand();
                else if (input == "D") DiscardCards();
                else if (input == "R") ResetGame();
                else if (int.TryParse(input, out int index) && index >= 1 && index <= CurrentHand.Count)
                {
                    ToggleSelection(CurrentHand[index - 1]);
                }
            }
        }

        private void ShowLiveScore()
        {
            int chips = 0;
            double multi = 1.0;

            if (_flush.IsMatch(_selectedCards))
            {
                chips += _flush.BaseChips;
                multi = _flush.BaseMultiplier;
                Console.WriteLine($"Combo: {_flush.Name} (+{_flush.BaseChips}, x{_flush.BaseMultiplier})");
            }

            foreach (var card in _selectedCards)
            {
                int val = Math.Min((int)card.Value, 10);
                if (card.Value == CardValue.A) val = 11;
                chips += val + card.CalculateBonus(_selectedCards);
                multi *= card.CalculateMultiplier(_selectedCards);
            }

            // SteelCard bonus
            foreach (var card in CurrentHand.Except(_selectedCards))
                if (card is SteelCard s) multi *= s.PassiveMultiplier;

            Console.WriteLine($"Aantal score: {chips * multi} <<<");
        }

        private void ToggleSelection(Card card)
        {
            if (_selectedCards.Contains(card)) _selectedCards.Remove(card);
            else if (_selectedCards.Count < 5) _selectedCards.Add(card);
        }

        private void PlayHand()
        {
            if (_selectedCards.Count == 0) return;

            int chips = 0;
            double multi = 1.0;
            if (_flush.IsMatch(_selectedCards)) { chips += _flush.BaseChips; multi = _flush.BaseMultiplier; }

            foreach (var card in _selectedCards)
            {
                int val = Math.Min((int)card.Value, 10);
                if (card.Value == CardValue.A) val = 11;
                chips += val + card.CalculateBonus(_selectedCards);
                multi *= card.CalculateMultiplier(_selectedCards);
            }

            foreach (var card in CurrentHand.Except(_selectedCards))
                if (card is SteelCard s) multi *= s.PassiveMultiplier;

            _totalScore += (chips * multi);

            foreach (var card in _selectedCards.ToArray())
            {
                if (card is GlassCard g && g.ShouldBreak()) Console.WriteLine($" {card} is helaas gebroken");
                _model.Hand.Cards.Remove(card);
            }

            _selectedCards.Clear();
            FillHand();
            Console.WriteLine("Hand gespeeld");
            Console.ReadKey();
        }

        private void DiscardCards()
        {
            foreach (var card in _selectedCards) _model.Hand.Cards.Remove(card);
            _selectedCards.Clear();
            FillHand();
        }

        private void ResetGame()
        {
            _model.Deck.Reset(); _model.Deck.Shuffle();
            _model.Hand.Cards.Clear(); _selectedCards.Clear();
            _totalScore = 0; FillHand();
        }

        private void FillHand()
        {
            while (CurrentHand.Count < _model.Hand.MaxCards)
            {
                var c = _model.Deck.TakeCard();
                if (c == null) break;
                _model.Hand.AddCard(c);
            }
        }
    }
}