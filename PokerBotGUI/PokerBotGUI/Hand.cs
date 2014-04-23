using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotGUI
{
    public class Hand
    {
        private Card[] hand;
        public Card this[int i]
        {
            get { return hand[i]; }
            set
            {
                hand[i] = value;
            }

        }

        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public Hand()
        {
            size = 5;
            hand = new Card[5];
        }

        public Card Get(int cardNum)
        {
            return hand[cardNum];
        }

        public void Set(Card c, int cardNum)
        {
            hand[cardNum] = c;
        }
    }
}
