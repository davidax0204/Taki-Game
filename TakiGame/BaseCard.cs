using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{

    class BaseCard
    {
        ConsoleColor colour;
        int value;
        int sideEffect = 0;
        public static ConsoleColor[] cardColors = { ConsoleColor.DarkRed , ConsoleColor.DarkYellow, ConsoleColor.DarkGreen, ConsoleColor.DarkBlue, ConsoleColor.DarkGray};

        public BaseCard(ConsoleColor colour, int value, int sideEffect)
        {
            this.colour = colour;
            this.value = value;
            this.sideEffect = sideEffect;
        }
        public BaseCard(ConsoleColor colour, int value)
        {
            this.colour = colour;
            this.value = value;
            this.sideEffect = 0;
        }
        public virtual string getCardValueByWords ()
        {
            return value.ToString()+" ";
        }
        public ConsoleColor getColour()
        {
            return this.colour;
        }
        public void setCoulor (ConsoleColor colour)
        {
            this.colour = colour;
        }
        public int getValue()
        {
            return this.value;
        }
        public virtual bool moveValidation(TakiGame game)
        {
            bool taki = moveTakiValidation(game);
            bool plus2 = movePlus2Validation(game);
            return plus2 && taki && (game.lastCard.getColour() == this.colour || game.lastCard.getValue() == this.value ||
                   game.lastCard.getColour() == BaseCard.cardColors[4]);
        }
        public void printCardValue ()
        {
            Console.BackgroundColor = this.colour;
            Console.Write("  ");
            Console.ResetColor();
            //Console.ForegroundColor = this.colour;
            Console.Write(" "  + this.getCardValueByWords() + " ");
            //Console.ResetColor();
            Console.BackgroundColor = this.colour;
            Console.Write("  ");
            Console.ResetColor();
            Console.Write("  ");
        }
        public void printCardCell ()
        {
            Console.BackgroundColor = this.colour;
            Console.Write("        ");
            Console.ResetColor();
            Console.Write("  ");
        }
        public virtual void cardSideEffect(TakiGame game)
        {
            return;
        }
        public virtual bool moveTakiValidation(TakiGame game)
        {
            if (game.isTakiOpen==false)
                return true;
            return game.lastCard.getColour() == this.colour;
        }
        public virtual bool movePlus2Validation(TakiGame game)
        {
            if (game.plus2Counter == 0)
                return true;
            return false;
        }
    }
}
