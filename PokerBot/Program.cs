using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck1 = new Deck();

            foreach (Card c in deck1.cards)
            {
                Console.WriteLine(c.ToString());
            }
        }
    }
}
