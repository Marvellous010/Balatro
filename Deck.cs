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

        public int RemainingCards => _cards.Count;

        public void Reset()
        {
            _cards.Clear();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (CardValue val in Enum.GetValues(typeof(CardValue)))
                {
                    _cards.Add(new Card(suit, val));
                }
            }

            MakeSpecialCards();
            AddStickers();
        }

        private void MakeSpecialCards()
        {
            for (int i = 0; i < 5; i++)
            {
                int index = _random.Next(_cards.Count);
                Card old = _cards[index];

                Card special = (i % 5) switch
                {
                    0 => new BonusCard(old.Suit, old.Value),
                    1 => new ExtraCard(old.Suit, old.Value),
                    2 => new GlassCard(old.Suit, old.Value),
                    3 => new WildCard(old.Suit, old.Value),
                    4 => new SteelCard(old.Suit, old.Value),
                    _ => old
                };

                _cards[index] = special;
            }
        }

        private void AddStickers()
        {
            for (int i = 0; i < 3; i++)
            {
                int index = _random.Next(_cards.Count);
                Card card = _cards[index];

                if (card is not StickerDecorator)
                {
                    int points = (_random.Next(1, 4)) * 5;
                    _cards[index] = new StickerDecorator(card, points);
                }
            }
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