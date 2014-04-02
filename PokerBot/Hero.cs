using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    public class Hero : Player
    {
        public Hero()
        {
            base.Name = "Hero";
        }

        public override int[] GetDiscards()
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

        public override bool Action(int pot, int draw, int pos)
        {
            return false;
        }
        
    }
}
