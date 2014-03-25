using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    public abstract class Player
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        protected Card[] hand;

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

        public void SortHand()
        {
            Card cur;
            int i, j;
            for (i = 1; i < hand.Length; i++)
            {
                cur = hand[i];
                j = i;

                while (j > 0 && (hand[j - 1] < cur))
                {
                    hand[j] = hand[j - 1];
                    j--;
                }
                hand[j] = cur;
            }
        }

        public static bool operator ==(Player a, Player b)
        {
            for (int i = 0; i < a.hand.Length; i++ )
            {
                if (a.hand[i] != b.hand[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(Player a, Player b)
        {
            return !(a == b);
        }


        //These are soo wrong, need more thought!
        // likely a series of if statements if nothing more inventive comes to mind
        public static bool operator >(Player a, Player b)
        {
            for (int i = 0; i < a.hand.Length; i++)
            {
                if (a.hand[i] <= b.hand[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator <(Player a, Player b)
        {
            return !(a > b && a == b);
        }

        public void DisplayHand()
        {
            foreach (Card c in hand)
            {
                Console.Write(c.ToString() + " ");
            }
            Console.WriteLine();
        }

        public abstract int[] GetDiscards();

        public override string ToString()
        {
            return Name;
        }
    }
}
