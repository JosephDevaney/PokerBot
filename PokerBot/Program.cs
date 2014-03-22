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

        static void initialise()
        {
            players = new Player[2];
            deck = new Deck();

            hero = new Hero();
            villain = new Villain();
            players[0] = hero;
            players[1] = villain;

            dealer = 0;
        }

        static int[] GetDiscards()
        {
            string cardsToDiscard;
            string[] cards;
            int[] discards;

            cardsToDiscard = Console.ReadLine();
            if (cardsToDiscard == "pat")
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
                    discards = GetDiscards();
                    if (discards != null)
                    {
                        hero.Discard(discards);
                        deck.Burn();
                        deck.DealDiscards(hero, discards);
                    }
                    
                    hero.DisplayHand();
                }

                Console.Write("Hero: ");
                hero.DisplayHand();

                Console.Write("Villain: ");
                villain.DisplayHand();

                Console.WriteLine("Press any key to continue!");
                Console.ReadKey();
            }
            
        }
    }
}
