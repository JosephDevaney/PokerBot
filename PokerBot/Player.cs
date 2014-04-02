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

        private int chipStack;

        public int ChipStack
        {
            get { return chipStack; }
            set { chipStack = value; }
        }

        private int contribToPot;

        public int ContribToPot
        {
            get { return contribToPot; }
            set { contribToPot = value; }
        }

        protected Hand hand;  //Hand hand

        public Player()
        {
            hand = new Hand(); //new Hand;
            chipStack = 100;
        }

        public void SetCard(Card c, int cardNum)
        {
            hand.Set(c, cardNum);
        }

        public void Discard(int[] discards)
        {
            for (int i = 0; i < discards.Length; i++ )
            {
                hand.Set(null, discards[i]);
            }
        }

        public int[] GetHandEval()
        {
            int[] res = HandEvaluator.HandValue(hand);
            return res;
        }

        public void SortHand()
        {
            Card cur;
            int i, j;
            for (i = 1; i < hand.Size; i++)
            {
                cur = hand.Get(i);  //cur = hand.Get(i);
                j = i;

                while (j > 0 && (hand.Get(j - 1) < cur))    //&& hand.Get(j-1) < cur
                {
                    hand.Set(hand.Get(j-1), j);
                    j--;
                }
                hand.Set(cur, j);
            }
        }

        public abstract bool Action(int pot, int draw, int pos);

        public int PostBlind(int blind)
        {
            chipStack -= blind;
            return blind;
        }


        #region Operators
        public static bool operator ==(Player a, Player b)
        {
            for (int i = 0; i < a.hand.Size; i++ )
            {
                if (a.hand.Get(i) != b.hand.Get(i))
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

        public static bool operator >(Player a, Player b)
        {
            int[] aHand = a.GetHandEval();
            int[] bHand = b.GetHandEval();

            if (a == b)
            {
                return false;
            }
            if (aHand[0] > bHand[0])
            {
                return true;
            }
            if (aHand[0] < bHand[0])
            {
                return false;
            }
            for (int i = 1; i < aHand.Length; i++ )
            {
                Card a1, b1;
                a1 = a.hand.Get(aHand[i]);
                b1 = b.hand.Get(bHand[i]);
                if (a1 < b1)    // a.hand.Get(aHand[i]) < b.hand.Get(bHand[i])
                {
                    return true;
                }
                else if (b1 < a1)
                {
                    return false;
                }
            }

            return false;
        }

        public static bool operator <(Player a, Player b)
        {
            return !(a > b && a == b);
        }
        #endregion

        /*
        #region HandEval
        public int HasStraighFlush()
        {
            if (HasFlush() && HasStraight() > 0)
            {
                return hand[0].Rank;
            }
            else
            {
                return 0;
            }
        }

        public int HasQuads()
        {
            for (int i = 0; i < hand.Length - 3; i++ )
            {
                if (hand[i] == hand[i+3])
                {
                    return hand[i].Rank;
                }
            }
            return 0;
        }

        public bool HasFlush()
        {
            for (int i = 0; i < hand.Length - 1; i++)
            {
                if (hand[i].Suit != hand[i + 1].Suit)
                {
                    return false;
                }
            }
            return true;
        }

        public int HasStraight()
        {
            for (int i = 0; i < hand.Length - 1; i++)
            {
                if (hand[i].Rank != hand[i + 1].Rank - 1)
                {
                    return 0;
                }
            }
            return hand[0].Rank;
        }

        public int HasTrips()
        {
            if (HasQuads() > 0)
            {
                return 0;
            }
            for (int i = 0; i < hand.Length - 2; i++ )
            {
                if (hand[i] == hand[i+2])
                {
                    return hand[i].Rank;
                }
            }
            return 0;
        }

        public int HasPair()
        {
            if (HasTrips() > 0)
            {
                return 0;
            }
            for (int i = 0; i < hand.Length - 1;  i++ )
            {
                if (hand[i] == hand[i + 1])
                {
                    return hand[i].Rank;
                }
            }
            return 0;
        }
        #endregion
         * */


        public void DisplayHand()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write(hand.Get(i).ToString() + " ");  
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
