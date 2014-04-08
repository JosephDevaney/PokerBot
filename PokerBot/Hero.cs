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

        public override int Action(int pot, int bet, int toCall)
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
                options.Add("raise");
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

            switch (input)
            {
                case "check":
                    return Check();

                case "call":
                    return Call(toCall);

                case "bet":
                    return Bet(bet);

                case "raise":
                    return Raise(bet);

                case "fold":
                    return Fold();
                
                default:
                    return 0;
            }
            
        }
        
    }
}
