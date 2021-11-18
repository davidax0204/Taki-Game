using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class KingCard : BaseCard
    {
        public KingCard() : base(BaseCard.cardColors[4], 19) { }

        public override bool moveValidation(TakiGame game)
        {
            bool taki = moveTakiValidation(game);
            return true && taki;
        }
        public override string getCardValueByWords()
        {
            return "K ";
        }
        public override void cardSideEffect(TakiGame game)
        {
            game.isKingActive = true;
            game.plus2Counter = 0;
        }
    }
}
