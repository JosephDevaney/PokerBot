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

        public override int Action(int pot, int bet, int toCall, int totalBets)
        {
            List<string> options = new List<string>();
            ActedThisRound = true;

            if ((ContribToPot * 2) == pot)
            {
                options.Add("check");
                options.Add("bet");
            }
            else if ((ContribToPot * 2) < pot)
            {
                options.Add("call");
                if (totalBets < 4)
                {
                    options.Add("raise");
                }
                
            }

            Random r = new Random();
            int rand = r.Next(options.Count);

            string s = options[rand];

            return MakeAction(s, bet, toCall);
            
        }
    }
}
