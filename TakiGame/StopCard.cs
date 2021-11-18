using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class StopCard : BaseCard
    {
        public StopCard(ConsoleColor colour) : base(colour,16) { }

        public override string getCardValueByWords()
        {
            return "S ";
        }
        public override bool moveValidation(TakiGame game)
        {
            bool plus2 = movePlus2Validation(game);
            return plus2 &&  (game.lastCard.getColour() == this.getColour() || game.lastCard.getValue() == this.getValue() ||
                   game.lastCard.getColour() == BaseCard.cardColors[4]); // or empty colour
        }
        public override void cardSideEffect(TakiGame game)
        {
            game.isStopNextPlayer = true;
        }
        public override bool movePlus2Validation(TakiGame game)
        {
            if (game.plus2Counter == 0)
                return true;
            return false;
        }
    }
}
