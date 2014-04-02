using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    public class Villain : Player
    {
        public Villain()
        {
            base.Name = "Villain";
        }

        public void Draw()
        {

        }

        public override int[] GetDiscards()
        {
            int[] discards = null;
            List<int> disc = new List<int>();
            int minRank = 9;
            SortHand();

            for (int i = 0; i < hand.Size; i++)
            {
                if (hand.Get(i).Rank > minRank)
                {
                    disc.Add(i);
                }
                else if (i > 0 && hand.Get(i) == hand.Get(i-1))
                {
                    disc.Add(i);
                }
            }

            discards = disc.ToArray();
            return discards;
        }

        public override bool Action(int pot, int draw, int pos)
        {
            if ((pot / 2) == ContribToPot && pos == 0)
            {
                Console.WriteLine("Check");
                return false;
            }
            if ((pot / 2) == ContribToPot && pos == 1)
            {
                Console.WriteLine("Check");
                return true;
            }
            if ((pot / 2) != ContribToPot && draw == 0)
            {
                ContribToPot += (pot - ContribToPot) - ContribToPot;    //Add the difference to ContribToPot
                Console.WriteLine("Call");
                return false;
            }
            
            return false;
        }
    }
}
