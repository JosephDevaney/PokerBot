using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    public class Card
    {
        /*
         * Cards have a suit and a rank
         * Both attributes will be represented by integers
         * 0-3 for Spades, Hearts, Diamonds, Clubs respectively
         * 2-14 for 2-10, J, Q, K, A
         * 
         * The Deck will be shuffled before the dealing of the hand commences
         * */

        private int suit;

        public int Suit
        {
            get { return suit; }
            set { suit = value; }
        }

        private int rank;

        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public Card()
        {
            suit = 0;
            rank = 0;
        }

        public Card(Random r)
        {
            suit = r.Next(3);
            rank = r.Next(1, 13);
        }

        public void Display()
        {

        }

        public static bool operator ==(Card a, Card b)
        {
            return a.Rank == b.Rank;
        }

        public static bool operator !=(Card a, Card b)
        {
            return !(a == b);
        }

        public static bool operator >(Card a, Card b)
        {
            return a.Rank > b.Rank;
        }

        public static bool operator >=(Card a, Card b)
        {
            return (a > b) || (a == b);
        }

        public static bool operator <(Card a, Card b)
        {
            return !(a >= b);
        }

        public static bool operator <=(Card a, Card b)
        {
            return !(a > b);
        }

        public override string ToString()
        {
            char s, r;
            if (suit == 0)
            {
                s = 'S';
            }
            else if (suit == 1)
            {
                s = 'H';
            }
            else if (suit == 2)
            {
                s = 'D';
            }
            else
            {
                s = 'C';
            }
            
            if (rank == 10)
            {
                r = 'T';
            }
            else if (rank == 11)
            {
                r = 'J';
            }
            else if (rank == 12)
            {
                r = 'Q';
            }
            else if (rank == 13)
            {
                r = 'K';
            }
            else if (rank == 14)
            {
                r = 'A';
            }
            else
            {
                r = Convert.ToChar(rank + 48);
            }

            return("" + r + s);
        }
    }
}
