using System;
using System.Text;
using Balatro1;

namespace Balatro1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = Encoding.UTF8;

            Model model = new Model(new Deck(), new PlayerHand(8));
            new ViewModel(model).Run();
        }
    }
}