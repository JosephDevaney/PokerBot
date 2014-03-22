using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    class Deck
    {
        public Card[] cards;

        public Deck()
        {
            cards = new Card[52];
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

        public void Shuffle()
        {

        }
    }
}
