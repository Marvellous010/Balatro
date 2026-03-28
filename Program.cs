using Balatro1;

namespace Balatro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck testDeck = new Deck();
            testDeck.Shuffle();

            PlayerHand hand = new PlayerHand(8);

            // Deal 5 regular cards
            for (int i = 0; i < 5; i++)
            {
                var card = testDeck.TakeCard();
                if (card == null) break;
                hand.AddCard(card);
            }

      
            // Regular cards
            hand.AddCard(new Card(Suit.Clubs, CardValue.Ten));
            hand.AddCard(new Card(Suit.Diamonds, CardValue.Ten));

            // Special cards
            hand.AddCard(new BonusCard(Suit.Hearts, CardValue.A));
            hand.AddCard(new ExtraCard(Suit.Spades, CardValue.Ten)); // Should give +6 (3 Tens in hand * 2)

            Model model = new Model(testDeck, hand);
            ViewModel viewModel = new ViewModel(model);
            viewModel.UpdateFromModel();

           
            viewModel.Run();
        }
    }
}