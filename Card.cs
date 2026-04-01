using System.Collections.Generic;

namespace Balatro1
{
    public class Card
    {
        public Suit Suit { get; }
        public CardValue Value { get; }
        public virtual bool IsWild => false;

        
        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        
        public virtual int CalculateBonus(IEnumerable<Card> hand) => 0;
        public virtual double CalculateMultiplier(IEnumerable<Card> hand) => 1.0;

        public override string ToString()
        {
            string suitSymbol = Suit switch
            {
                Suit.Hearts => "♥",
                Suit.Diamonds => "♦",
                Suit.Clubs => "♣",
                Suit.Spades => "♠",
                _ => Suit.ToString()
            };

            string valStr = Value switch
            {
                CardValue.Two => "2",
                CardValue.Three => "3",
                CardValue.Four => "4",
                CardValue.Five => "5",
                CardValue.Six => "6",
                CardValue.Seven => "7",
                CardValue.Eight => "8",
                CardValue.Nine => "9",
                CardValue.Ten => "10",
                _ => Value.ToString() 
            };

           
            string type = this.GetType().Name switch
            {
                "BonusCard" => " (B)",
                "ExtraCard" => " (E)",
                "GlassCard" => " (G)",
                "WildCard" => " (W)",
                "SteelCard" => " (S)",
                _ => ""
            };

            return $"{valStr}{suitSymbol}{type}";
        }
    }
}