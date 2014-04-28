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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokerBotGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Window tableWindow;
        private string screenName;

        public string ScreenName
        {
            get { return screenName; }
            set { screenName = value; }
        }

        private int heroStack;

        public int HeroStack
        {
            get { return heroStack; }
            set { heroStack = value; }
        }

        private int villainStack;

        public int VillainStack
        {
            get { return villainStack; }
            set { villainStack = value; }
        }

        private int betSize;

        public int BetSize
        {
            get { return betSize; }
            set { betSize = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            ScreenName = "Hero";
            HeroStack = 100;
            VillainStack = 100;
            BetSize = 2;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            tableWindow = new TableWindow(ScreenName, HeroStack, VillainStack, BetSize);
            tableWindow.Show();
            this.Hide();
        }
    }
}
