using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class Plus3BrokenCard : BaseCard
    {
        public Plus3BrokenCard ():base(BaseCard.cardColors[4], 13,3) { }

        public override bool moveValidation(TakiGame game)
        {
            bool taki = moveTakiValidation(game);
            bool plus2 = movePlus2Validation(game);
            return plus2 && true && taki;

        }
        public override string getCardValueByWords()
        {
            return "!3";
        }
        public override void cardSideEffect(TakiGame game)
        {
            game.plus3BrokenPunishment = true;
        }
        public override bool movePlus2Validation(TakiGame game)
        {
            if (game.plus2Counter == 0)
                return true;
            return false;
        }
    }
}
