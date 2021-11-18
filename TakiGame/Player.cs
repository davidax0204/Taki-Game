using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class Player
    {
        protected List<BaseCard> playerDeck = new List<BaseCard>();

        string name;
        int id;
        bool isActive;
        public Player (string name, int id)
        {
            this.name = name;
            this.id = id;
        }
        public int getId ()
        {
            return this.id;
        }
        public void printPlayerDeck()
        {
            if (isActive)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("{0} ", this.name);
            Console.ResetColor();
            Console.WriteLine();
            string str = "";
            for (int i = 0; i < playerDeck.Count; i++)
            {
                if (i<10)
                    str = str + "    " + (i+1) + "     ";
                else
                    str = str + "  " + (i+1) + "      ";
            }
            Console.WriteLine(str);
            str = "";
            for (int i = 0; i < playerDeck.Count; i++)
            {
                playerDeck[i].printCardCell();
            }
            Console.WriteLine();
            for (int i = 0; i < playerDeck.Count; i++)
            {
                playerDeck[i].printCardValue();
            }
            Console.WriteLine();
            for (int i = 0; i < playerDeck.Count; i++)
            {
                playerDeck[i].printCardCell();
            }
            
            Console.WriteLine();
            Console.WriteLine();
        }
        public void setActive(bool value)
        {
            isActive = value;
        }
        public bool isActivePlayer ()
        {
            return isActive;
        }
        public string getName()
        {
            return this.name;
        }
        public List<BaseCard> getPlayerCards()
        {
            return playerDeck;
        }
    }


}
