using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class Plus2Card : BaseCard
    {
        public Plus2Card (ConsoleColor colour) : base(colour,12,2) { }

        public override bool moveValidation(TakiGame game)
        {
            bool taki = moveTakiValidation(game);

            return taki && (game.lastCard.getColour() == this.getColour() || game.lastCard.getValue() == this.getValue()) ||
                            game.lastCard.getColour() == BaseCard.cardColors[4];
        }
        public override string getCardValueByWords()
        {
            return "+2";
        }
        public override void cardSideEffect(TakiGame game)
        {
            game.plus2Counter++;
        }
    }
}
