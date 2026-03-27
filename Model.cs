namespace Balatro1
{
    public class Model
    {
        public Deck Deck { get; }
        public PlayerHand Hand { get; }

        public Model(Deck deck, PlayerHand hand)
        {
            Deck = deck;
            Hand = hand;
        }
    }
}