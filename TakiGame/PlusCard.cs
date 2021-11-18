using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class PlusCard : BaseCard
    {
        public PlusCard (ConsoleColor colour) :base(colour,15) { }

        public override bool moveValidation(TakiGame game)
        {
            bool plus2 = movePlus2Validation(game);
            return plus2 && (game.lastCard.getColour() == this.getColour() || game.lastCard.getValue()==this.getValue());
        }
        public override string getCardValueByWords()
        {
            return "+ ";
        }
        public override void cardSideEffect(TakiGame game)
        {
            game.isPlusOpen = true;
        }
        public override bool movePlus2Validation(TakiGame game)
        {
            if (game.plus2Counter == 0)
                return true;
            return false;
        }
    }
}
