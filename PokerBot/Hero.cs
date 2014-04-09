using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    public class Hero : Player
    {
        public Hero()
        {
            base.Name = "Hero";
        }

        public override int[] GetDiscards()
        {
            string cardsToDiscard;
            string[] cards;
            int[] discards;

            cardsToDiscard = Console.ReadLine();
            if (cardsToDiscard == "pat" || cardsToDiscard == "")
            {
                discards = null;
            }
            else
            {
                cards = cardsToDiscard.Split(' ');
                discards = new int[cards.Length];

                for (int i = 0; i < cards.Length; i++)
                {
                    discards[i] = Convert.ToInt32(cards[i]);
                }
            }

            return discards;
        }

        public override int Action(int pot, int bet, int toCall, int totalBets)
        {
            List<string> options = new List<string>();
            string input = "";
            ActedThisRound = true;

            if ((ContribToPot * 2) == pot)
            {
                options.Add("check");
                options.Add("bet");
            }
            else if ((ContribToPot * 2) < pot)
            {
                options.Add("fold");
                options.Add("call");
                if (totalBets < 4)
                {
                    options.Add("raise");
                }
            }


            while (options.Contains(input) == false)
            {
                Console.Write("Action:");
                foreach (string s in options)
                {
                    Console.Write("\t" + s);
                }
                Console.WriteLine();
                input = Console.ReadLine().ToLower();

                if (options.Contains(input) == false)
                {
                    Console.WriteLine("Input Error");
                }
            }

            return MakeAction(input, bet, toCall);
            
        }
        
    }
}
