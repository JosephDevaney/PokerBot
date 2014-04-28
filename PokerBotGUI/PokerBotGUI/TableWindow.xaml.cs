using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PokerBotGUI
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public Table table;
        private string discards;

        private Card heroCard0;

        public Card HeroCard0
        {
            get { return heroCard0; }
            set { heroCard0 = value; }
        }

        public TableWindow(string name, int hStack, int vStack, int bet)
        {
            InitializeComponent();
            table = new Table(name, hStack, vStack, bet);
            this.DataContext = table;
            discards = "";
        }

        private void fold_button(object sender, RoutedEventArgs e)
        {
            table.hero.input = "fold";
        }

        private void raise_button(object sender, RoutedEventArgs e)
        {
            table.hero.input = "raise";
        }

        private void call_button(object sender, RoutedEventArgs e)
        {
            table.hero.input = "call";
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            table.Play();
        }

        private void check_button(object sender, RoutedEventArgs e)
        {
            table.hero.input = "check";
        }

        private void bet_button(object sender, RoutedEventArgs e)
        {
            table.hero.input = "bet";
        }

        private void discard_button(object sender, RoutedEventArgs e)
        {
            if (discards == "")
            {
                table.hero.cardsToDiscard = "pat";
            }
            else
            {
                table.hero.cardsToDiscard = discards;
            }
            discards = "";
        }

        private void card0_Click(object sender, RoutedEventArgs e)
        {
            discards += "0 ";
        }

        private void card1_Click(object sender, RoutedEventArgs e)
        {
            discards += "1 ";
        }

        private void card2_Click(object sender, RoutedEventArgs e)
        {
            discards += "2 ";
        }

        private void card3_Click(object sender, RoutedEventArgs e)
        {
            discards += "3 ";
        }

        private void card4_Click(object sender, RoutedEventArgs e)
        {
            discards += "4 ";
        }
    }
}
