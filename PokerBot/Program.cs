using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBot
{
    class Program
    {
        public static Table table;

        static void initialise()
        {
            table = new Table();
        }

        static void Main(string[] args)
        {
            initialise();

            while (true)
            {
                table.Play();
            }
            
        }
    }
}
