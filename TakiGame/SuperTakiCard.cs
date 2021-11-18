using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class SuperTakiCard : BaseCard
    {
        public SuperTakiCard() : base(BaseCard.cardColors[4], 18) { }

        public override string getCardValueByWords()
        {
            return "ST";
        }
        public override bool moveValidation(TakiGame game)
        {

            bool plus2 = movePlus2Validation(game);
            return plus2 && true;
        }
        public override void cardSideEffect(TakiGame game)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SUPER TAKI! What colour do you the SUPER TAKI");
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
                        game.isTakiOpen = true;
                        this.setCoulor(ConsoleColor.DarkRed);
                        break;
                    }
                case "B":
                    {
                        game.isTakiOpen = true;
                        this.setCoulor(ConsoleColor.DarkBlue);
                        break;
                    }
                case "G":
                    {
                        game.isTakiOpen = true;
                        this.setCoulor(ConsoleColor.DarkGreen);
                        break;
                    }
                case "Y":
                    {
                        game.isTakiOpen = true;
                        this.setCoulor(ConsoleColor.DarkYellow);
                        break;
                    }
            }
        }
        public bool changeColorCardInputValidation(string str)
        {
            str = str.Replace(" ", "");
            str = str.ToUpper();
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
