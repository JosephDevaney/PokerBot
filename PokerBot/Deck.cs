using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    public class Deck
    {
        public Card[] cards;

        private int top;

        public Deck()
        {
            cards = new Card[52];
            top = 51;
            int index = 0;

            for (int k = 0; k < cards.Length; k++)
            {
                cards[k] = new Card();
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    cards[index].Suit = i;
                    cards[index].Rank = j;
                    index++;
                }
            }
        }

        public void DisplayDeck()
        {
            foreach (Card c in cards)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public void Shuffle()
        {
            /* Start with simple shuffle algorithm
             * Improve in later versions
             */
            top = 51;
            
            Random r = new Random();

            for (int i = cards.Length - 1; i > 0; i-- )
            {
                int n = r.Next(i + 1);

                Card temp = cards[i];
                cards[i] = cards[n];
                cards[n] = temp;
            }
        }

        public void Deal(Player[] players, int dealer)
        {
            int player = dealer;
            for (int i = 0; i < 5; i++ )
            {
                for (int j = 0; j < players.Length; j++ )
                {
                    player = NextPlayer(player, players.Length);
                    players[player].SetCard(cards[top--], i);
                }
            }
        }

        public int NextPlayer(int player, int size)
        {
            return (player = (player + 1) % size);
        }

        public void DealDiscards(Player p, int[] discards)
        {
            for (int i = 0; i < discards.Length; i++)
            {
                p.SetCard(cards[top--], discards[i]);
            }
        }

        public void Burn()
        {
            top--;
        }
    }
}
