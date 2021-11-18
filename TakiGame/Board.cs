using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class Board
    {
        private List<BaseCard> cardDeck = new List<BaseCard>();

        public Board()
        {
            for (int indexDeck = 0; indexDeck < 2; indexDeck++)
            {
                for (int indexColour = 0; indexColour < 4; indexColour++)
                {
                    for (int indexCardValue = 1; indexCardValue < 10; indexCardValue++)
                    {
                        cardDeck.Add(new BaseCard(BaseCard.cardColors[indexColour],indexCardValue));
                    }
                    cardDeck.Add(new Plus2Card(BaseCard.cardColors[indexColour]));
                    cardDeck.Add(new PlusCard(BaseCard.cardColors[indexColour]));
                    cardDeck.Add(new StopCard(BaseCard.cardColors[indexColour]));
                    cardDeck.Add(new ChangeDirectionCard(BaseCard.cardColors[indexColour]));
                    cardDeck.Add(new TakiCard(BaseCard.cardColors[indexColour]));
                }
                cardDeck.Add(new KingCard());
                cardDeck.Add(new ChangeColourCard());
                cardDeck.Add(new SuperTakiCard());
                cardDeck.Add(new Plus3Card());
                cardDeck.Add(new Plus3BrokenCard());
            }
        }
        public List<BaseCard> getDeckCards()
        {
            return cardDeck;
        }
        public void giveThePlayerCards(int count, Player player)
        {
            for (int i = 0; i < count; i++)
            {
                Random randomCard = new Random();
                int indexRandom = randomCard.Next(0, cardDeck.Count);
                BaseCard card = cardDeck[indexRandom];            
                player.getPlayerCards().Add(card);
                cardDeck.Remove(cardDeck[indexRandom]);
            }
        }
        public BaseCard openNewCard ()
        {
            Random randomCard = new Random();
            int indexRandom = randomCard.Next(0, cardDeck.Count);
            BaseCard card = cardDeck[indexRandom];
            cardDeck.Remove(cardDeck[indexRandom]);
            return card;
        }
        public BaseCard openFirstCard()
        {
            BaseCard card = null;
            while (true)
            {
                Random randomCard = new Random();
                int indexRandom = randomCard.Next(0, cardDeck.Count);
                card = cardDeck[indexRandom];
                if (card.getValue() >= 10)
                    continue;
                cardDeck.Remove(cardDeck[indexRandom]);
                break;
            }
            return card;
        }
        public static int inputValidation(string input, Player player, TakiGame game)
        {
            string str = input.Replace(" ", "");
            str = str.ToUpper();
            if (str == "TAKE" && game.isTakiOpen==false)
                return -100;

            if (game.isTakiOpen)
            {
                if (inputValidationTaki(input) == true)
                {
                    return -200;
                }
            }

            bool flag =int.TryParse(str, out int strNumber);
            if (flag == true && strNumber > 0 && strNumber <= player.getPlayerCards().Count)
                return strNumber;

            return -1;
        }
        public static bool inputValidationTaki(string input)
        {
            string str = input.Replace(" ", "");
            str = str.ToUpper();
            if (str == "CLOSE")
                return true;

            return false;
        }
    }
}
