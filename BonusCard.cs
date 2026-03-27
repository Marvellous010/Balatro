namespace Balatro1
{
    public class BonusCard : Card
    {
        public BonusCard(Suit suit, CardValue value) : base(suit, value) { }
        public override int BonusPoints => 10;
        public override string ToString() => $"[BONUS] {Value} of {Suit} (+10)";
    }
}