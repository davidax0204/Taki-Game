using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class TakiCard : BaseCard
    {
        public TakiCard(ConsoleColor colour) : base(colour,17) { }

        public override string getCardValueByWords()
        {
            return "T ";
        }
        public override bool moveValidation(TakiGame game)
        {
            bool plus2 = movePlus2Validation(game);
            return plus2 && game.lastCard.getColour() == this.getColour() || game.lastCard.getValue() == this.getValue() ||
                   game.lastCard.getColour() == BaseCard.cardColors[4]; // or empty colour
        }
        public override void cardSideEffect(TakiGame game)
        {
            game.isTakiOpen = true;

        }
        public override bool movePlus2Validation(TakiGame game)
        {
            if (game.plus2Counter == 0)
                return true;
            return false;
        }
    }
}
