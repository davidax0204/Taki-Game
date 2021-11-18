using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class ChangeColourCard : BaseCard
    {
        public ChangeColourCard () :base(BaseCard.cardColors[4],10) { }

        public override bool moveValidation(TakiGame game)
        {
            bool taki = moveTakiValidation(game);
            bool plus2 = movePlus2Validation(game);
            return plus2 && taki && true;
        }
        public override string getCardValueByWords()
        {
            return "CC";
        }
        public override void cardSideEffect(TakiGame game)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("To what colour do you want to change?");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("(R)ed");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("   (B)lue");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("   (Y)ellow");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("   (G)reen");
            Console.ResetColor();
            Console.WriteLine();

            string input = Console.ReadLine().ToUpper();

            if (changeColorCardInputValidation(input) == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("The input is invalid, Please try again");
                Console.ResetColor();
                cardSideEffect(game);
            }

            switch (input)
            {
                case "R":
                    {
                        this.setCoulor(ConsoleColor.DarkRed);
                        break;
                    }
                case "B":
                    {
                        this.setCoulor(ConsoleColor.DarkBlue);
                        break;
                    }
                case "G":
                    {
                        this.setCoulor(ConsoleColor.DarkGreen);
                        break;
                    }
                case "Y":
                    {
                        this.setCoulor(ConsoleColor.DarkYellow);
                        break;
                    }
            }
        }
        public bool changeColorCardInputValidation (string str)
        {
            str = str.Replace(" ", "");
            str =str.ToUpper();
            if (str.Length != 1)
                return false;
            if (str == "R" || str == "B" || str == "Y" || str == "G")
                return true;
            return false;
        }
        public override bool movePlus2Validation(TakiGame game)
        {
            if (game.plus2Counter == 0)
                return true;
            return false;
        }

    }
}
