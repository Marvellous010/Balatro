using System;
using System.Collections.Generic;

namespace Balatro1
{
    public class GameEngine
    {
        private Model _model;
        private List<Card> _selectedCards = new();
        private double _totalScore = 0;
        private readonly ScoreCalculator _calculator = new ScoreCalculator();
        private readonly HandEvaluator _evaluator = new HandEvaluator();

        public List<Card> CurrentHand => _model.Hand.Cards;
        public List<Card> SelectedCards => _selectedCards;
        public double TotalScore => _totalScore;
        public int RemainingCards => _model.Deck.RemainingCards;

        public GameEngine(Model model)
        {
            _model = model;
        }

        public void Reset()
        {
            _model.Deck.Reset();
            _model.Deck.Shuffle();
            _model.Hand.Cards.Clear();
            _selectedCards.Clear();
            _totalScore = 0;
            FillHand();
        }

        public void ToggleSelection(Card card)
        {
            if (_selectedCards.Contains(card))
                _selectedCards.Remove(card);
            else if (_selectedCards.Count < 5)
                _selectedCards.Add(card);
        }

        public (int chips, double multiplier) GetCurrentScore()
        {
            return _calculator.Calculate(_selectedCards, CurrentHand);
        }

        public ICombination? GetCurrentCombination()
        {
            return _evaluator.GetBestCombination(_selectedCards);
        }

        public void PlayHand()
        {
            if (_selectedCards.Count == 0) return;

            var (chips, multi) = GetCurrentScore();
            _totalScore += chips * multi;

            foreach (var card in _selectedCards.ToArray())
            {
                if (card is GlassCard g && g.ShouldBreak())
                    Console.WriteLine($"{card} is gebroken!");
                _model.Hand.Cards.Remove(card);
            }

            _selectedCards.Clear();
            FillHand();
        }

        public void DiscardCards()
        {
            foreach (var card in _selectedCards)
                _model.Hand.Cards.Remove(card);
            _selectedCards.Clear();
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