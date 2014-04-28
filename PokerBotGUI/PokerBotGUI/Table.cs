using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace PokerBotGUI
{
    public class Table : INotifyPropertyChanged
    {
        public Player[] players { get; set; }
        public Deck deck { get; set; }
        public Player hero { get; set; }
        public Player villain { get; set; }
        public int dealer;
        public int curPlay;
        private int pot;

        public int Pot
        {
            get { return pot; }
            set
            {
                if (value != pot)
                {
                    pot = value;
                    OnPropertyChanged("Pot");
                }
            }
        }
        public int smallBet;
        private int betsThisRound;

        public Table() 
            : this("Hero", 100, 100, 2)
        {

        }
        

        public Table(string name, int hStack, int vStack, int betSize)
        {
            players = new Player[2];
            deck = new Deck();

            hero = new Hero(name);
            villain = new Villain();

            hero.ChipStack = hStack;
            villain.ChipStack = vStack;

            players[0] = hero;
            players[1] = villain;

            dealer = 0;
            curPlay = dealer;
            pot = 0;
            smallBet = betSize;
            betsThisRound = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        public int NextPlayer(int player)
        {
            return player = (player + 1) % players.Length;
        }

        public Player Winner()
        {
            Player winner = players[0];
            for (int i = 1; i < players.Length; i++)
            {
                if (players[i] > winner)
                {
                    winner = players[i];
                }
            }

            winner.ChipStack += pot;
            return winner;
        }

        public bool ActionComplete()
        {
            bool allActedThisRound = true;
            bool allContribEqual = false;

            foreach (Player p in players)
            {
                if (p.ActedThisRound == false)
                {
                    allActedThisRound = false;
                }
            }
            if ((hero.ContribToPot * 2) == pot)
            {
                allContribEqual = true;
            }

            if (allActedThisRound && allContribEqual)
            {
                return true;
            }

            return false;
        }

        public void ResetHand()
        {
            villain.ShowHand = false;
            foreach (Player p in players)
            {
                p.ContribToPot = 0;
                p.ActedThisRound = false;
                p.NumBets = 0;
                if (p.ChipStack > 0)
                {
                    p.AllIn = false;
                }
            }
            Pot = 0;
        }

        public void RefundDiff(int p, int refund)
        {
            p = NextPlayer(p);
            players[p].ContribToPot -= refund;
            players[p].ChipStack += refund;
        }

        public void WinByDefault(int cur)
        {
            cur = NextPlayer(cur);
            players[cur].ChipStack += pot;

            ResetHand();

            dealer = NextPlayer(dealer);
        }

        public void ShowPlayer(Player p)
        {
            Console.WriteLine(p + ": " + p.ChipStack);
        }

        public void Play()
        {
            int[] discards;
            int bet;
            int actionResult = 0;

            while (true)
            {
                deck.Shuffle();

                Pot = players[dealer].PostBlind(smallBet / 2);
                curPlay = NextPlayer(dealer);
                Pot += players[curPlay].PostBlind(smallBet);
                players[curPlay].NumBets++;

                if (Object.ReferenceEquals(players[dealer], villain))
                {
                    villain.VillainAction = "Posts Small Blind";
                    villain.ShowAction = true;
                }
                else
                {
                    villain.VillainAction = "Posts Big Blind";
                    villain.ShowAction = true;
                }

                deck.Burn();
                deck.Deal(players, dealer);

                curPlay = dealer;
                betsThisRound = 0;
                while (!ActionComplete())
                {
                    if ((hero.AllIn || villain.AllIn) && (hero.ContribToPot == villain.ContribToPot))
                    {
                        break;
                    }
                    actionResult = players[curPlay].Action(pot, smallBet, Math.Abs(hero.ContribToPot - villain.ContribToPot), betsThisRound);
                    if (actionResult == -1)
                    {
                        WinByDefault(curPlay);
                        break;
                    }
                    else if (players[curPlay].AllIn && (players[curPlay].ContribToPot * 2) < Pot)
                    {
                        RefundDiff(curPlay, Math.Abs(hero.ContribToPot - villain.ContribToPot));
                    }

                    Pot += actionResult;
                    betsThisRound = hero.NumBets + villain.NumBets;
                    curPlay = NextPlayer(curPlay);
                }
                if (actionResult == -1)
                {
                    continue;
                }


                for (int i = 0; i < 3; i++)
                {
                    deck.Burn();
                    curPlay = NextPlayer(dealer);
                    for (int j = 0; j < players.Length; j++)
                    {
                        discards = players[curPlay].GetDiscards();
                        if (discards != null)
                        {
                            players[curPlay].Discard(discards);
                            if (Object.ReferenceEquals(players[curPlay], villain))
                            {
                                villain.VillainAction = "Discards " + discards.Length + " cards";
                            }
                            deck.DealDiscards(players[curPlay], discards);
                        }
                        else 
                        {
                            if (Object.ReferenceEquals(players[curPlay], villain))
                            {
                                villain.VillainAction = "Stands Pat";
                            }
                        }
                        curPlay = NextPlayer(curPlay);
                    }

                    if (hero.AllIn || villain.AllIn)
                    {
                        continue;
                    }

                    curPlay = NextPlayer(dealer);

                    if (i == 0)
                    {
                        bet = smallBet;
                    }
                    else
                    {
                        bet = smallBet * 2;
                    }

                    foreach (Player p in players)
                    {
                        p.ActedThisRound = false;
                        p.NumBets = 0;
                    }
                    betsThisRound = 0;
                    while (!ActionComplete())
                    {
                        if ((hero.AllIn || villain.AllIn) && (hero.ContribToPot == villain.ContribToPot))
                        {
                            break;
                        }
                        actionResult = players[curPlay].Action(pot, bet, Math.Abs(hero.ContribToPot - villain.ContribToPot), betsThisRound);
                        if (actionResult == -1)
                        {
                            WinByDefault(curPlay);
                            break;
                        }

                        Pot += actionResult;
                        betsThisRound = hero.NumBets + villain.NumBets;
                        
                        if (players[curPlay].AllIn && (players[curPlay].ContribToPot * 2) < Pot)
                        {
                            RefundDiff(curPlay, Math.Abs(hero.ContribToPot - villain.ContribToPot));
                        }

                        curPlay = NextPlayer(curPlay);
                    }
                    if (actionResult == -1)
                    {
                        break;
                    }
                }

                if (actionResult == -1)
                {
                    continue;
                }

                hero.SortHand();

                villain.SortHand();
                villain.ShowHand = true;

                if (hero == villain)
                {
                    hero.ChipStack += pot / 2;
                    villain.ChipStack += pot / 2;
                }
                else
                {
                    Winner();
                }

                int a = 100;
                while (a > 0)
                {
                    Yield(100000);
                    a--;
                }
                ResetHand();

                dealer = NextPlayer(dealer);
            }
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
