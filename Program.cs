using Balatro1;

namespace Balatro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck testDeck = new Deck();
            testDeck.Shuffle();

            // Create a hand that can hold 6 cards (5 from deck + 1 bonus)
            PlayerHand hand = new PlayerHand(6);

            // Deal 5 regular cards
            for (int i = 0; i < 5; i++)
            {
                var card = testDeck.TakeCard();
                if (card == null) break;
                hand.AddCard(card);
            }

            // Manually add a BonusCard for debugging
            hand.AddCard(new BonusCard(Suit.Hearts, CardValue.A));

            Model model = new Model(testDeck, hand);
            ViewModel viewModel = new ViewModel(model);
            viewModel.UpdateFromModel();

            // Run rendering logic directly from ViewModel
            viewModel.Run();
        }
    }
}