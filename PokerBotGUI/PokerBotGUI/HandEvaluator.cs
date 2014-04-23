using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotGUI
{
    public class HandEvaluator
    {
        /* Value is a list storing the number of times a Card occurs in a hand
         * Key is a list storing the Rank of the card
         * Index is a list that stores the index of the card in the hand array
         * 
         * hist is an array with the number of occurences in the first row
         * and the index of the card associated with that in the 2nd row
         * 
         * */
        public static int[,] Histogram(Hand hand)
        {
            List<int> key = new List<int>();
            List<int> value = new List<int>();
            List<int> index = new List<int>();
            int i, j, curK, curV, curI;

            for (i = 0; i < hand.Size; i++)
            {
                if (key.Contains(hand.Get(i).Rank))
                {
                    int k = key.FindIndex(x => x == hand.Get(i).Rank);
                    value[k]++;
                }
                else
                {
                    key.Add(hand.Get(i).Rank);
                    value.Add(1);
                    index.Add(i);
                }
            }

            for (i = 1; i < key.Count; i++)
            {
                curI = index[i];
                curK = key[i];
                curV = value[i];

                j = i;

                while (j > 0 && (value[j - 1] < curV))
                {
                    index[j] = index[j - 1];
                    key[j] = key[j - 1];
                    value[j] = value[j - 1];
                    j--;
                }
                index[j] = curI;
                key[j] = curK;
                value[j] = curV;
            }

            int[,] hist = new int[2, key.Count];
            for (i = 0; i < key.Count; i++)
            {
                hist[0, i] = value[i];
                hist[1, i] = index[i];
            }
            return hist;
        }

        /* Hand Value will return an int signifying the value of the hand
         * 1 is a Straigh Flush
         * 2 is Four-of-a-Kind
         * 3 is a Full House
         * 4 is a Flush
         * 5 is a straight
         * 6 is Three-of-a-Kind
         * 7 is Two Pair
         * 8 is One Pair
         * 9 is High Card
         * */

        public static int[] HandValue(Hand hand)
        {
            //int handValue = 0;
            int[,] hist = Histogram(hand);
            int[] handValue = new int[hist.GetLength(1) + 1];

            if (hist[0, 0] == 4)    // 4-of-a-kind
            {
                handValue[0] = 2;
            }
            else if (hist[0, 0] == 3 && hist[0, 1] == 2)    // Full-House
            {
                handValue[0] = 3;
            }
            else if (hist[0, 0] == 3 && hist[0, 1] == 1)    // 3-of-a-kind
            {
                handValue[0] = 6;
            }
            else if (hist[0, 0] == 2 && hist[0, 1] == 2)    // 2 Pair
            {
                handValue[0] = 7;
            }
            else if (hist[0, 0] == 2 && hist[0, 1] == 1)    // 1 Pair
            {
                handValue[0] = 8;
            }
            else
            {
                handValue[0] = StraightOrFlush(hist, hand);
            }

            for (int i = 1; i < handValue.Length; i++)
            {
                handValue[i] = hist[1, i - 1];
            }

            return handValue;
        }

        private static int StraightOrFlush(int[,] hist, Hand hand)
        {
            bool isFlush = true;
            bool isStraight = false;

            for (int i = 0; i < hand.Size - 1; i++)
            {
                if (hand.Get(i).Suit != hand.Get(i + 1).Suit)
                {
                    isFlush = false;
                }
            }

            if (hand.Get(0).Rank - hand.Get(hand.Size - 1).Rank == 4)
            {
                isStraight = true;
            }

            if (isFlush && isStraight)
            {
                return 1;   // Straight Flush
            }
            else if (isFlush)
            {
                return 4;   // Flush
            }
            else if (isStraight)
            {
                return 5;   // Straight
            }
            else
            {
                return 9;   // High Card
            }

        }
    }
}
