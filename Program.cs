using Balatro1;

namespace Balatro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck testDeck = new Deck();
            testDeck.Shuffle();

            PlayerHand hand = new PlayerHand(10);
            for (int i = 0; i < 5; i++)
            {
                var card = testDeck.TakeCard();
                if (card != null) hand.AddCard(card);
            }

           
            hand.AddCard(new Card(Suit.Clubs, CardValue.Ten));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.Ten));
            hand.AddCard(new BonusCard(Suit.Hearts, CardValue.A));
            hand.AddCard(new ExtraCard(Suit.Spades, CardValue.Ten));
            hand.AddCard(new GlassCard(Suit.Diamonds, CardValue.Eight));

            Model model = new Model(testDeck, hand);
            ViewModel viewModel = new ViewModel(model);
            viewModel.UpdateFromModel();
            viewModel.Run();
        }
    }
}