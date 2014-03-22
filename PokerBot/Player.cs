using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    class Player
    {
        private int[] hand;

        public int[] Hand
        {
            get { return hand; }
            set { hand = value; }
        }

        public Player()
        {
            hand = new int[5];
        }
    }
}
