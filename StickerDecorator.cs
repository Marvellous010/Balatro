using System.Collections.Generic;

namespace Balatro1
{
    public class StickerDecorator : Card
    {
        private Card _card;
        private int _extraPoints;

        public StickerDecorator(Card card, int extraPoints)
            : base(card.Suit, card.Value)
        {
            _card = card;
            _extraPoints = extraPoints;
        }

        public override int CalculateBonus(IEnumerable<Card> hand)
            => _card.CalculateBonus(hand) + _extraPoints;

        public override double CalculateMultiplier(IEnumerable<Card> hand)
            => _card.CalculateMultiplier(hand);

    }
}