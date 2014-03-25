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

            for (int i = 0; i < hand.Length; i++)
            {
                if (hand[i].Rank > minRank)
                {
                    disc.Add(i);
                }
                else if (i > 0 && hand[i] == hand[i-1])
                {
                    disc.Add(i);
                }
            }

            discards = disc.ToArray();
            return discards;
        }
    }
}
