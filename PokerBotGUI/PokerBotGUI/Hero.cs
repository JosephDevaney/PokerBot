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
                if (ChipStack < bet)
                {
                    bet = ChipStack;
                }
            }
            else if ((ContribToPot * 2) < pot)
            {
                options.Add("fold");
                options.Add("call");
                if (ChipStack < toCall)
                {
                    toCall = ChipStack;
                }
                if (totalBets < 4 && ChipStack > toCall)
                {
                    options.Add("raise");
                    RaiseBtn = true;
                    if (ChipStack < (toCall + bet))
                    {
                        bet = ChipStack - toCall;
                    }
                }
                FoldCall = true;
            }


            while (options.Contains(input) == false)
            {
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
