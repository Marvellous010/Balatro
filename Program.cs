using Balatro1;

namespace Balatro1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialisatie van de Model-laag
            Deck deck = new Deck();
            PlayerHand hand = new PlayerHand(8); // Standaard handgrootte van 8

            Model model = new Model(deck, hand);

            // Start de ViewModel (Game Loop)
            ViewModel game = new ViewModel(model);
            game.Run();
        }
    }
}