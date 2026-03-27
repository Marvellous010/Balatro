using Balatro1;

Console.WriteLine("--- Initializing Deck ---");
Deck deck = new Deck();
Console.WriteLine($"Deck created with {deck.RemainingCards} cards.");

Console.WriteLine("\n--- Shuffling ---");
deck.Shuffle();

Console.WriteLine("\n--- Dealing 5 cards ---");
for (int i = 0; i < 5; i++)
{
    var card = deck.Deal();
    if (card != null)
    {
        Console.WriteLine($"Dealt: {card}");
    }
}

Console.WriteLine($"\nRemaining cards in deck: {deck.RemainingCards}");