using System;
using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{
    public class Deck
    {
        private List<Card> _cards = new();
        private readonly Random _random = new();

        public Deck() => Reset();

        public void Reset()
        {
            _cards.Clear();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                foreach (CardValue val in Enum.GetValues(typeof(CardValue)))
                    _cards.Add(new Card(suit, val));
        }

        public void Shuffle() => _cards = _cards.OrderBy(_ => _random.Next()).ToList();

        public Card? TakeCard()
        {
            if (_cards.Count == 0) return null;
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }
}