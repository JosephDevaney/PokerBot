using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    public class Table
    {
        public Player[] players;
        public Deck deck;
        public Player hero;
        public Player villain;
        public int dealer;
        public int curPlay;
        public int pot;
        public int smallBet;
        private int bettingRound;
        //public bool allActedThisRound;

        public Table()
        {
            players = new Player[2];
            deck = new Deck();

            hero = new Hero();
            villain = new Villain();
            players[0] = hero;
            players[1] = villain;

            dealer = 0;
            curPlay = dealer;
            pot = 0;
            smallBet = 2;
            bettingRound = 0;
            //allActedThisRound = false;
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
            foreach (Player p in players)
            {
                p.ContribToPot = 0;
                p.ActedThisRound = false;
            }
            pot = 0;
        }

        public void Play()
        {
            int[] discards;
            int bet;

            while (true)
            {
                Console.Clear();
                deck.Shuffle();

                pot = players[dealer].PostBlind(smallBet / 2);
                curPlay = NextPlayer(dealer);
                pot += players[curPlay].PostBlind(smallBet);

                deck.Burn();
                deck.Deal(players, dealer);
                hero.DisplayHand();

                curPlay = dealer;
                while (!ActionComplete())
                {
                    pot += players[curPlay].Action(pot, smallBet, Math.Abs(hero.ContribToPot - villain.ContribToPot));
                    curPlay = NextPlayer(curPlay);
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
                            deck.DealDiscards(players[curPlay], discards);
                            Console.WriteLine(players[curPlay].Name + " draws " + discards.Length + " cards.");
                        }
                        else
                        {
                            Console.WriteLine(players[curPlay].Name + " stands pat.");
                        }
                        curPlay = NextPlayer(curPlay);
                    }

                    hero.DisplayHand();
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
                    }
                    while (!ActionComplete())
                    {
                        pot += players[curPlay].Action(pot, bet, Math.Abs(hero.ContribToPot - villain.ContribToPot));
                        curPlay = NextPlayer(curPlay);
                    }

                    
                }

                hero.SortHand();
                Console.Write("\n\nHero: ");
                hero.DisplayHand();

                villain.SortHand();
                Console.Write("Villain: ");
                villain.DisplayHand();

                if (hero == villain)
                {
                    Console.WriteLine("\n\nIt's a draw, both players win!");
                    hero.ChipStack += pot / 2;
                    villain.ChipStack += pot / 2;
                }
                else
                {
                    Console.WriteLine("\n\n" + Winner() + " wins");
                }

                Console.WriteLine("\nPress any key to continue!");
                Console.ReadKey();

                ResetHand();

                dealer = NextPlayer(dealer);
            }
        }
    }
}
