namespace Balatro1
{
    public class WildCard : Card
    {
        public WildCard(Suit suit, CardValue value) : base(suit, value) { }

        public override bool IsWild => true;

        public override string ToString() => $"[WILD] {Value} of {Suit}";
    }
}