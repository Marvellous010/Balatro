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
        private readonly ICombination _pair = new PairCombination();

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
                Console.WriteLine($"Score: {_totalScore}");
                Console.WriteLine($"Deck: {_model.Deck.RemainingCards} | Hand: {CurrentHand.Count}");
                Console.WriteLine("-----------------------------------");

                for (int i = 0; i < CurrentHand.Count; i++)
                {
                    string check = _selectedCards.Contains(CurrentHand[i]) ? "[X]" : "[ ]";
                    Console.WriteLine($"{check} {i + 1}: {CurrentHand[i]}");
                }

                Console.WriteLine("-----------------------------------");

                if (_selectedCards.Count > 0) ShowScore();
                else Console.WriteLine("Kies een kaart");

                Console.WriteLine("\n[1-8] Kies | [S]peel | [D]iscard | [R]eset | [Q]uit");
                string input = Console.ReadLine()?.ToUpper();

                if (string.IsNullOrWhiteSpace(input)) continue;

                if (input == "Q") playing = false;
                else if (input == "S") PlayHand();
                else if (input == "D") DiscardCards();
                else if (input == "R") ResetGame();
                else if (int.TryParse(input, out int nr) && nr >= 1 && nr <= CurrentHand.Count)
                    ToggleSelection(CurrentHand[nr - 1]);
            }
        }

        private (int, double) CalculateScore()
        {
            int chips = 0;
            double multi = 1.0;

            if (_flush.IsMatch(_selectedCards))
            {
                chips += _flush.BaseChips;
                multi = _flush.BaseMultiplier;
            }
            else if (_pair.IsMatch(_selectedCards))
            {
                chips += _pair.BaseChips;
                multi = _pair.BaseMultiplier;
            }

            foreach (var card in _selectedCards)
            {
                int val = Math.Min((int)card.Value, 10);
                if (card.Value == CardValue.A) val = 11;
                chips += val + card.CalculateBonus(_selectedCards);
                multi *= card.CalculateMultiplier(_selectedCards);
            }

            foreach (var card in CurrentHand.Except(_selectedCards))
                if (card is SteelCard s) multi *= s.PassiveMultiplier;

            return (chips, multi);
        }

        private void ShowScore()
        {
            var (chips, multi) = CalculateScore();

            if (_flush.IsMatch(_selectedCards))
                Console.WriteLine($"Combo: {_flush.Name} (+{_flush.BaseChips}, x{_flush.BaseMultiplier})");
            else if (_pair.IsMatch(_selectedCards))
                Console.WriteLine($"Combo: {_pair.Name} (+{_pair.BaseChips}, x{_pair.BaseMultiplier})");

            Console.WriteLine($"Score: {chips * multi}");
        }

        private void ToggleSelection(Card card)
        {
            if (_selectedCards.Contains(card)) _selectedCards.Remove(card);
            else if (_selectedCards.Count < 5) _selectedCards.Add(card);
        }

        private void PlayHand()
        {
            if (_selectedCards.Count == 0) return;

            var (chips, multi) = CalculateScore();
            _totalScore += chips * multi;

            foreach (var card in _selectedCards.ToArray())
            {
                if (card is GlassCard g && g.ShouldBreak())
                    Console.WriteLine($"{card} is gebroken!");
                _model.Hand.Cards.Remove(card);
            }

            _selectedCards.Clear();
            FillHand();
            Console.WriteLine("Gespeeld!");
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
            _model.Deck.Reset();
            _model.Deck.Shuffle();
            _model.Hand.Cards.Clear();
            _selectedCards.Clear();
            _totalScore = 0;
            FillHand();
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