using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    public class Player
    {
        private Card[] hand;

        public Player()
        {
            hand = new Card[5];
        }

        public void SetCard(Card c, int cardNum)
        {
            hand[cardNum] = c;
        }

        public void Discard(int[] discards)
        {
            for (int i = 0; i < discards.Length; i++ )
            {
                hand[discards[i]] = null;
            }
        }

        public void DisplayHand()
        {
            foreach (Card c in hand)
            {
                Console.Write(c.ToString() + " ");
            }
            Console.WriteLine();
        }
    }
}
