using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace PokerBotGUI
{
    public class Hero : Player
    {
        public Hero()
        {
            base.Name = "Hero";
            base.input = "";
            cardsToDiscard = "";
        }

        public override int[] GetDiscards()
        {
            cardsToDiscard = "";
            string[] cards;
            int[] discards;

            while (cardsToDiscard == "")
            {
                Yield(100);
            }

            /*cardsToDiscard = Console.ReadLine();*/
            if (cardsToDiscard == "pat")
            {
                discards = null;
            }
            else
            {
                cards = cardsToDiscard.Split(' ');
                cards = cards.Distinct().ToArray();
                discards = new int[cards.Length - 1];

                for (int i = 0; i < discards.Length; i++)
                {
                    discards[i] = Convert.ToInt32(cards[i]);
                }
            }

            return discards;
        }

        public override int Action(int pot, int bet, int toCall, int totalBets)
        {
            List<string> options = new List<string>();
            input = "";
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
//                 Console.Write("Action:");
//                 foreach (string s in options)
//                 {
//                     Console.Write("\t" + s);
//                 }
//                 Console.WriteLine();
//                 input = Console.ReadLine().ToLower();
// 
//                 if (options.Contains(input) == false)
//                 {
//                     Console.WriteLine("Input Error");
//                 }
                Yield(100);
            }

            return MakeAction(input, bet, toCall);

        }

        private void Yield(long ticks)
        {

            // Note: a tick is 100 nanoseconds

            long dtEnd = DateTime.Now.AddTicks(ticks).Ticks;

            while (DateTime.Now.Ticks < dtEnd)
            {

                MainWindow.tableWindow.Dispatcher.Invoke(DispatcherPriority.Background, (DispatcherOperationCallback)delegate(object unused) { return null; }, null);

            }

        }
    }
}
