using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    class Program
    {
        public static Player[] players;
        public static Deck deck;
        static Player hero;
        static Player villain;
        static int dealer;
        static int curPlay;

        static void initialise()
        {
            players = new Player[2];
            deck = new Deck();

            hero = new Hero();
            villain = new Villain();
            players[0] = hero;
            players[1] = villain;

            dealer = 0;
            curPlay = dealer;
        }

        static int NextPlayer(int player)
        {
            return player = (player + 1) % players.Length;
        }

        static Player Winner()
        {
            Player winner = players[0];
            for (int i = 1; i < players.Length; i++ )
            {
                if (players[i] > winner)
                {
                    winner = players[i];
                }
            }
            return winner;
        }

        static void Main(string[] args)
        {
            initialise();
            int[] discards;

            while (true)
            {
                Console.Clear();
                deck.Shuffle();
                deck.Burn();
                deck.Deal(players, dealer);
                hero.DisplayHand();

                for (int i = 0; i < 3; i++)
                {
                    deck.Burn();
                    curPlay = NextPlayer(dealer);
                    for (int j = 0; j < players.Length; j++ )
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
                }
                else
                {
                    Console.WriteLine("\n\n" + Winner() + " wins");
                }

                Console.WriteLine("\nPress any key to continue!");
                Console.ReadKey();

                dealer = NextPlayer(dealer);
            }
            
        }
    }
}
