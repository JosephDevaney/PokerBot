using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace PokerBotGUI
{
    public class Hero : Player
    {
        public Hero(string name)
        {
            base.Name = name;
            base.input = "";
            cardsToDiscard = "";
            TurnBtnsOff();
        }

        public Hero() 
            : this("Hero")
        {

        }

        public void TurnBtnsOff()
        {
            FoldCall = false;
            RaiseBtn = false;
            CheckBet = false;
            DiscardBtn = false;
        }

        public override int[] GetDiscards()
        {
            cardsToDiscard = "";
            string[] cards;
            int[] discards;

            DiscardBtn = true;

            while (cardsToDiscard == "")
            {
                Yield(100);
            }
            DiscardBtn = false;
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
                CheckBet = true;
            }
            else if ((ContribToPot * 2) < pot)
            {
                options.Add("fold");
                options.Add("call");
                if (totalBets < 4)
                {
                    options.Add("raise");
                    RaiseBtn = true;
                }
                FoldCall = true;
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

            TurnBtnsOff();
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
