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
            //int numDisc = 0;
            //int index = 0;
            int minRank = 8;
            SortHand();

//             for (int i = 0; i < hand.Length; i++ )
//             {
//                 if (hand[i].Rank > minRank)
//                 {
//                     numDisc++;
//                 }
//             }

            for (int i = 0; i < hand.Length; i++)
            {
                if (hand[i].Rank > minRank)
                {
                    disc.Add(i);
                }
                else if (i > 1 && hand[i].Rank == hand[i-1].Rank)
                {
                    disc.Add(i);
                }
            }

            //disc = disc.Distinct().ToList();
            discards = disc.ToArray();
            return discards;
        }
    }
}
