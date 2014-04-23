using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PokerBotGUI
{
    public abstract class Player : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string input;

        public string cardsToDiscard;

        private int chipStack;

        public int ChipStack
        {
            get { return chipStack; }
            set { chipStack = value; OnPropertyChanged("ChipStack"); }
        }

        private int contribToPot;

        public int ContribToPot
        {
            get { return contribToPot; }
            set { contribToPot = value; }
        }

        private int numBets;

        public int NumBets
        {
            get { return numBets; }
            set { numBets = value; }
        }

        private bool actedThisRound;

        public bool ActedThisRound
        {
            get { return actedThisRound; }
            set { actedThisRound = value; }
        }

        public Hand hand;  //Hand hand

        #region Card Properties
        private Card card0;

        public Card Card0
        {
            get { return card0; }
            set { card0 = value; OnPropertyChanged("Card0"); }
        }

        private Card card1;

        public Card Card1
        {
            get { return card1; }
            set { card1 = value; OnPropertyChanged("Card1"); }
        }

        private Card card2;

        public Card Card2
        {
            get { return card2; }
            set { card2 = value; OnPropertyChanged("Card2"); }
        }

        private Card card3;

        public Card Card3
        {
            get { return card3; }
            set { card3 = value; OnPropertyChanged("Card3"); }
        }

        private Card card4;

        public Card Card4
        {
            get { return card4; }
            set { card4 = value; OnPropertyChanged("Card4"); }
        }
        #endregion

        public Player()
        {
            hand = new Hand(); //new Hand;
            chipStack = 100;
            actedThisRound = false;

            Card0 = hand[0];
            Card1 = hand[1];
            Card2 = hand[2];
            Card3 = hand[3];
            Card4 = hand[4];

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        public void SetCard(Card c, int cardNum)
        {
            hand[cardNum] = c;
            if (cardNum == 0)
            {
                Card0 = c;
            }
            else if (cardNum == 1)
            {
                Card1 = c;
            }
            else if (cardNum == 2)
            {
                Card2 = c;
            }
            else if (cardNum == 3)
            {
                Card3 = c;
            }
            else if (cardNum == 4)
            {
                Card4 = c;
            }
        }

        public void Discard(int[] discards)
        {
            for (int i = 0; i < discards.Length; i++)
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
                    hand.Set(hand.Get(j - 1), j);
                    j--;
                }
                hand.Set(cur, j);
            }
        }

        public abstract int Action(int pot, int bet, int toCall, int totalBets);

        public int PostBlind(int blind)
        {
            ChipStack -= blind;
            ContribToPot += blind;
            return blind;
        }

        public int Check()
        {
            return 0;
        }

        public int Call(int toCall)
        {
            ChipStack -= toCall;
            ContribToPot += toCall;
            return toCall;
        }

        public int Bet(int bet)
        {
            ChipStack -= bet;
            ContribToPot += bet;
            NumBets++;
            return bet;
        }

        public int Raise(int bet, int toCall)
        {
            int total = 0;
            total += Call(toCall);
            total += Bet(bet);
            return total;
        }

        public int Fold()
        {
            return -1;
        }

        public int MakeAction(string s, int bet, int toCall)
        {
            switch (s)
            {
                case "check":
                    Console.WriteLine("Check");
                    return Check();

                case "call":
                    Console.WriteLine("Call");
                    return Call(toCall);

                case "bet":
                    Console.WriteLine("Bet");
                    return Bet(bet);

                case "raise":
                    Console.WriteLine("Raise");
                    return Raise(bet, toCall);

                case "fold":
                    Console.WriteLine("Fold");
                    return Fold();

                default:
                    return 0;
            }
        }

        #region Operators
        public static bool operator ==(Player a, Player b)
        {
            for (int i = 0; i < a.hand.Size; i++)
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
            for (int i = 1; i < aHand.Length; i++)
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
