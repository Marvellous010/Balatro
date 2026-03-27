using System.Collections.Generic;

namespace Balatro1
{
    public class PlayerHand
    {
        public List<Card> Cards { get; } = new();
        public int MaxCards { get; }

        public PlayerHand(int maxCards) => MaxCards = maxCards;

        public void AddCard(Card card)
        {
            if (Cards.Count < MaxCards) Cards.Add(card);
        }
    }
}