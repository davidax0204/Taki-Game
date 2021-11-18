using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class ChangeDirectionCard : BaseCard
    {
        public ChangeDirectionCard (ConsoleColor colour) : base(colour,11) { }

        public override bool moveValidation(TakiGame game)
        {
            bool plus2 = movePlus2Validation(game);
            return plus2 && game.lastCard.getColour() == this.getColour() || game.lastCard.getColour() == BaseCard.cardColors[4] ||
                   game.lastCard.getValue() == this.getValue();

        }
        public override string getCardValueByWords()
        {
            return "CD";
        }
        public override void cardSideEffect (TakiGame game)
        {
            game.isFoward = game.isFoward == true ? false : true;
        }
        public override bool movePlus2Validation(TakiGame game)
        {
            if (game.plus2Counter == 0)
                return true;
            return false;
        }
    }
}
