﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotGUI
{
    public class Villain : Player
    {
        public Villain()
        {
            base.Name = "Villain";
            VillainAction = "";
            ShowAction = false;
            ShowHand = false;
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
                else if (i > 0 && hand.Get(i) == hand.Get(i - 1))
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
                options.Add("Check");
                options.Add("Bet");
                if (ChipStack < bet)
                {
                    bet = ChipStack;
                }
            }
            else if ((ContribToPot * 2) < pot)
            {
                options.Add("Call");
                if (ChipStack < toCall)
                {
                    toCall = ChipStack;
                }
                if (totalBets < 4 && ChipStack > toCall)
                {
                    options.Add("Raise");
                    if (ChipStack < (toCall + bet))
                    {
                        bet = ChipStack - toCall;
                    }
                }

            }

            Random r = new Random();
            int rand = r.Next(options.Count);

            string s = options[rand];

            VillainAction = String.Copy(s);
            s = s.ToLower();

            return MakeAction(s, bet, toCall);

        }
    }
}
