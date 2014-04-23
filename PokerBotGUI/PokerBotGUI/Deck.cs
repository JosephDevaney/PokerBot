using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace PokerBotGUI
{
    public class Deck : INotifyPropertyChanged
    {
        public Card[] cards;

        private MyImage[] cardImages;

        public MyImage[] CardImages
        {
            get { return cardImages; }
        }

        private Card deckBack;

        public Card DeckBack
        {
            get { return deckBack; }
            set { deckBack = value; OnPropertyChanged("DeckBack"); }
        }

        private int top;

        public Deck()
        {
            cards = new Card[52];
            cardImages = new MyImage[52];
            DeckBack = new Card();

            LoadDeckImages();
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
                    cards[index].Image = cardImages[index];
                    index++;
                }
            }
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

        public void LoadDeckImages()
        {
            int index = 0;
            foreach (string filename in
                System.IO.Directory.GetFiles
                ("C:\\Users\\Journeyman\\Documents\\DT2282\\OOP\\Sem2Assignment\\Graphics\\PlayingCards\\PlayingCards")) //Make folder in project for these
            {
                if (filename.Contains("pokerstars"))
                {
                    cardImages[index++] = new MyImage(new BitmapImage(new Uri(filename)), System.IO.Path.GetFileNameWithoutExtension(filename));
                }
            }

            string backPath = "C:\\Users\\Journeyman\\Documents\\DT2282\\OOP\\Sem2Assignment\\Graphics\\back.jpeg";
            DeckBack.Image = new MyImage(new BitmapImage(new Uri(backPath)), "back.jpeg");
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

            for (int i = cards.Length - 1; i > 0; i--)
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
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < players.Length; j++)
                {
                    player = NextPlayer(player, players.Length);
                    players[player].SetCard(cards[top--], i);
//                     if (Object.ReferenceEquals(players[player], hero))
//                     {
//                         hero.SetCard(cards[top--], i);
//                     }
//                     else
//                     {
//                         villain.SetCard(cards[top--], i);
//                     }
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
