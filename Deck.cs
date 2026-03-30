using System;
using System.Collections.Generic;
using System.Linq;

namespace Balatro1
{

    public class Deck
    {
        private List<Card> _cards = new();
        private readonly Random _random = new();

        public Deck() => Reset(); // DIT MOET TERUG

        public int RemainingCards => _cards.Count;

        public void Reset()
        // ...
        {
            _cards.Clear();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (CardValue val in Enum.GetValues(typeof(CardValue)))
                {

             
                    // Define specific unique special cards in the deck
                    if (suit == Suit.Hearts && val == CardValue.A)
                        _cards.Add(new BonusCard(suit, val));
                    else if (suit == Suit.Spades && val == CardValue.Ten)
                        _cards.Add(new ExtraCard(suit, val));
                    else if (suit == Suit.Diamonds && val == CardValue.Eight)
                        _cards.Add(new GlassCard(suit, val));
                    else if (suit == Suit.Clubs && val == CardValue.J)
                        _cards.Add(new WildCard(suit, val));
               
                    else if (suit == Suit.Spades && val == CardValue.K)
                        _cards.Add(new SteelCard(suit, val)); 
           
                    else
                        _cards.Add(new Card(suit, val));
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