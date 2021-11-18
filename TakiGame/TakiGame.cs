using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace TakiGame
{
    class TakiGame
    {
        public Board board;
        public List<Player> players;
        public List<Player> playersWithBrokenPlus3Card = new List<Player>();
        public BaseCard lastCard;
        public List<BaseCard> removeCards = new List<BaseCard>();
        public Player lastPlayer;

        public bool isFoward = true;
        public bool isStopNextPlayer = false;
        public bool isTakiOpen = false;
        public bool isPlusOpen = false;
        public bool isPlus3open = false;
        public bool isPlus3openSituation = false;
        public bool plus3BrokenPunishment = false;
        public bool isKingActive = false;
        public int plus2Counter;

        public void play ()
        {
            // starting the game for the first time
            board = new Board();
            players = new List<Player>();
            Console.SetWindowSize(200, 50);

            // creating 4 players and giving them cards
            allPlayersCreation();

            // activating the first player
            activateFirstPlayer();

            BaseCard chosenCard = null;

            // first card is being taken from the deck to be shown on the desk
            this.lastCard = board.openFirstCard();

            // the loop that the whole game is running there
            while (true)
            {
                // printing all player cards 
                PrintAllPlayerCards();

                // printing the last open card
                printingLastCard();

                // telling the players if any special cards is still active 
                announcementOfSpecialCards();

                // A messege will be shown if the deck is empty
                deckIsEmptyMessege();

                // Asking for input 
                Console.WriteLine("{0}, please put your wanted card or take card from the deck 'take' or 'close' if TAKI is still open", this.lastPlayer.getName());
                string input = Console.ReadLine();

                // Validating the input by input validation (not card value 0-8)
                int validatedInput = Board.inputValidation(input, lastPlayer, this);

                // if the player decided to close the taki (he enter the word 'close' when the taki is active) and activating the last card of the taki
                if (validatedInput == -200)  // 'close'
                {
                    takeLastCardSideEffect(chosenCard);
                    continue;
                }

                // the input that the player entered is not valid (0-8)
                else if (validatedInput == -1)  // invalid input
                {
                    wrongInput0_8();
                    continue;
                }

                // the player decided to pick a card 
                else if (validatedInput == -100 && isTakiOpen != true) // 'take'
                {
                    if (plus2Counter > 0 && validatedInput == -100) // player takes card by multiplier of 2 when PLUS 2 is active
                    {
                        for (int i = 0; i < plus2Counter * 2; i++)
                        {
                            takeCard();
                        }
                        plus2Counter = 0;
                        changePlayer();
                        continue;
                    }
                    else  // takes a card from the deck if he just decided to take a card without a reason or he doesnt have any other card to play validly 
                    {
                        takeCard();
                        changePlayer();
                        continue;
                    }
                }
                // the chosen card that the player decided to use
                chosenCard = lastPlayer.getPlayerCards()[validatedInput - 1];

                // We are checking which card is the chosed card and act accordingly
                if (chosenCard.moveValidation(this) == true)
                {
                    // if taki card is not active so do sideeffect of each card
                    if (isTakiOpen == false)
                    {
                        chosenCard.cardSideEffect(this);
                    }

                    // if Plus3 active so check about broken 3 card
                    if (isPlus3open == true)
                    {
                        brokenCardSaving(chosenCard);
                    }

                    // stop card is chosed
                    if (this.isStopNextPlayer == true)
                    {
                        doStop(chosenCard);
                    }

                    // DEFUALT WAY !!! (if no one has 3 broken card)
                    else
                    {
                        continueWithout3Plus(chosenCard);
                    }

                    // if those situations doesnt happen so we have to change player
                    if ((isTakiOpen == false) && (isPlusOpen == false) && (isKingActive == false))
                    {
                        changePlayer();
                    }

                    // check for winners
                    if (win() == true)
                    {
                        Console.WriteLine("{0} has WON!!!", winningPlayer().getName());
                        break;
                    }

                    // status change
                    isKingActive = false;
                    isPlusOpen = false;
                    continue;
                }
                else // the player tried to put a wrong card that cannot be put in that way
                {
                    wrongMove();
                    continue;
                }
            }

            Console.WriteLine("GAME OVER");
        }
        // Taking a card from the deck and giving it to the last player
        public void takeCard()
        {
            if (board.getDeckCards().Count > 0)
            {
                BaseCard newCard = board.openNewCard();
                lastPlayer.getPlayerCards().Add(newCard);
            }
        }

        // make all the not active player take cards
        public void allPlayersTake3Cards (Player notActivePlayer)
        {
            if (board.getDeckCards().Count > 0)
            {
                BaseCard newCard = board.openNewCard();
                notActivePlayer.getPlayerCards().Add(newCard);
            }
        }

        // Error that a card is put in a way that cant be done
        public void wrongMove ()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("************** Wrong colour or wroung value *************");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Error messege for bad validation
        public void wrongInput0_8()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("************** Invalid Input *************");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        // doing the last card of the taki side effect
        public void takeLastCardSideEffect(BaseCard chosenCard)
        {
            plus2Counter = 0;
            isTakiOpen = false;
            if (chosenCard is SuperTakiCard)
            {
                changePlayer();
                return;
            }
            if (chosenCard is TakiCard)
            {
                changePlayer();
                return;
            }

            if (chosenCard != null)
            {
                chosenCard.cardSideEffect(this);
            }

            if (chosenCard is PlusCard)
            {
                isPlusOpen = false;
                return;
            }
            if (chosenCard is StopCard)
            {
                doStop(chosenCard);
                changePlayer();
                return;
            }

            if (chosenCard is ChangeDirectionCard)
            {
                changePlayer();
                return;
            }

            if (chosenCard is ChangeColourCard)
            {
                changePlayer();
                return;
            }

            changePlayer();
            return;
        }

        // showing a messege that the deck is empty and there is no way to take cards anymore
        public void deckIsEmptyMessege()
        {
            if (!(board.getDeckCards().Count > 0))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("There are no card left in the deck, if you will press 'take' you will skip your move");
                Console.ResetColor();
            }
        }

        // activate the first player
        public void activateFirstPlayer()
        {
            lastPlayer = players[0];
            lastPlayer.setActive(true);
        }

        // creating 4 players and giving them cards
        public void allPlayersCreation()
        {
            for (int indexPlayers = 0; indexPlayers < 4; indexPlayers++)
            {
                Player player = new Player("Player " + (indexPlayers + 1), indexPlayers);   // added +1 to the index
                players.Add(player);
                board.giveThePlayerCards(8, player);
            }
        }

        // telling the players if any special cards is still active 
        public void announcementOfSpecialCards()
        {
            if (this.isTakiOpen) // messege that taki is active
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("TAKI is still open");
                Console.ResetColor();
            }
            if (this.isPlusOpen) // messege that PLUS is active
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("PLUS is still open");
                Console.ResetColor();
            }
            if (this.plus2Counter > 0)  // messege that +2 card is active
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+2 is still open");
                Console.ResetColor();
            }
        }

        // printing the last open card
        public void printingLastCard()
        {
            Console.WriteLine();
            Console.WriteLine("Open Card :");
            Console.WriteLine();

            lastCard.printCardCell();
            Console.WriteLine();
            lastCard.printCardValue();
            Console.WriteLine();
            lastCard.printCardCell();
            Console.WriteLine();
            Console.WriteLine();
        }

        // doing the Side effect of the card STOP
        public void doStop(BaseCard chosenCard)
        {
            addToRemovedCards(lastCard);
            lastPlayer.getPlayerCards().Remove(chosenCard);
            lastCard = chosenCard;
            changePlayer();
            isStopNextPlayer = false;
        }

        // BaseCard cardPlus3 = null; // TO DO GET CARD BROKEN FROM THIS PLAYER.
        public BaseCard getBrokenCardFromPlayer(Player player)
        {
            BaseCard cardPlus3 = null;
            for (int indexCard = 0; indexCard < player.getPlayerCards().Count; indexCard++)
            {
                if (player.getPlayerCards()[indexCard] is Plus3BrokenCard)
                {
                    cardPlus3 = player.getPlayerCards()[indexCard];
                    break;
                }
            }
            return cardPlus3;
        }

        // input validation for broken 3 plus card
        public int validation3BrokenCard(string input)
        {
            string str = input.Replace(" ", "");
            str = str.ToUpper();
            if (str == "YES")
                return 1;
            else if (str == "NO")
                return 0;
            return -1;
        }

        // check if there a broken 3card and put the player that has this card in a list
        public bool isThereABrokenCardAndFillThemInList()
        {
            bool flagIsBrokenExist = false;
            playersWithBrokenPlus3Card.Clear();
            // if exist broken card in one of the player decks
            for (int indexPlayer = 0; indexPlayer < players.Count; indexPlayer++)
            {
                if (players[indexPlayer].isActivePlayer() != true)
                {
                    Player player = players[indexPlayer];
                    for (int indexCard = 0; indexCard < player.getPlayerCards().Count; indexCard++)
                    {
                        if (player.getPlayerCards()[indexCard] is Plus3BrokenCard)
                        {
                            flagIsBrokenExist = true;
                            playersWithBrokenPlus3Card.Add(player);
                            break;
                        }
                    }
                }
            }
            return flagIsBrokenExist;
        }

        // get all not active player list
        public List<Player> getAllNotActivePlayers()
        {
            List<Player> allNotActivePlayers = new List<Player>();

            for (int indexPlayer = 0; indexPlayer < players.Count; indexPlayer++)
            {
                if (players[indexPlayer].isActivePlayer() != true)
                {
                    allNotActivePlayers.Add(players[indexPlayer]);
                }
            }

            return allNotActivePlayers;
        }

        // get the last card that was removed
        public BaseCard getLastCardRemoved()
        {
            BaseCard lastRemovedCard = removeCards[removeCards.Count - 1];
            lastRemovedCard.cardSideEffect(this);
            return lastRemovedCard;
        }

        // changing player after each turn
        public void changePlayer()
        {

            int idx = lastPlayer.getId();
            players[idx].setActive(false);

            if (isFoward == true)
            {
                if (idx == 3)
                    idx = 0;
                else
                    idx++;
            }
            else
            {
                if (idx == 0)
                    idx = 3;
                else
                    idx--;
            }

            lastPlayer = players[idx];
            lastPlayer.setActive(true);
        }

        // adding the last open card to the list of removed cards
        public void addToRemovedCards(BaseCard lastCard)
        {
            removeCards.Add(lastCard);
        }

        // printing all the player cards
        public void PrintAllPlayerCards()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].printPlayerDeck();
            }
        }

        // find the active player that is playing now
        public Player findActivePlayer()
        {
            Player player = players[0];
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].isActivePlayer() == true)
                {
                    player = players[i];
                    break;
                }
            }
            return player;
        }

        // checking if anyone won
        public bool win()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].getPlayerCards().Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        // picking the winning player
        public Player winningPlayer()
        {
            Player result = null;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].getPlayerCards().Count == 0)
                {
                    result = players[i];
                    break;
                }
            }
            return result;
        }

        // if a plus 3 card is used so ask if anyone want to use the broken plus card 
        public void brokenCardSaving(BaseCard chosenCard)
        {
            isPlus3openSituation = true;
            bool isAgree = false;
            int indexAgreePlayer = -1;
            if (isThereABrokenCardAndFillThemInList() == true) // if there is a player that has the broken card in his deck
            {
                // list with player with broken card
                for (int indexPlayers = 0; indexPlayers < playersWithBrokenPlus3Card.Count; indexPlayers++)
                {
                    Player playerWit3BrokenCard = playersWithBrokenPlus3Card[indexPlayers];
                    int result;

                    while (true)
                    {
                        Console.WriteLine("{0}, Do you want to use your BROKEN 3 PLUS card 'yes' or 'no'?", playerWit3BrokenCard.getName());
                        string inputFor3PlusBrokenCard = Console.ReadLine();
                        result = validation3BrokenCard(inputFor3PlusBrokenCard); // input
                        if (result == -1)
                        {
                            Console.WriteLine("wrong input! please try again");
                            continue;
                        }
                        else if (result == 1) // YES
                        {
                            isAgree = true;
                            indexAgreePlayer = indexPlayers;
                            break;

                        }
                        else
                        {
                            break;
                        }
                    }
                    if (isAgree)
                        break;
                }

                if (!isAgree) // no one aggress to put the card and all getting 3 cards each!
                {
                    List<Player> notActivePlayers = getAllNotActivePlayers();
                    for (int i = 0; i < notActivePlayers.Count; i++)
                    {
                        for (int i3 = 0; i3 < 3; i3++)
                        {
                            allPlayersTake3Cards(notActivePlayers[i]);
                        }
                    }
                    lastPlayer.getPlayerCards().Remove(chosenCard);
                    isPlus3open = false;
                }
                else // someone has aggreed to use the broken card
                {

                    Player playerWit3BrokenCard = playersWithBrokenPlus3Card[indexAgreePlayer];
                    BaseCard cardPlus3 = getBrokenCardFromPlayer(playerWit3BrokenCard);
                    addToRemovedCards(cardPlus3);
                    lastPlayer.getPlayerCards().Remove(chosenCard);

                    // the player aggreed to play the card and the owner of the 3plus is taking 3 cards
                    playerWit3BrokenCard.getPlayerCards().Remove(cardPlus3);

                    isPlus3open = false;
                    // giving the player that used the Plus3 card 3 cards as a punishment
                    for (int i3 = 0; i3 < 3; i3++)
                    {
                        takeCard();
                    }
                }
            }
            else
            { // no has the card broken 3 
                List<Player> notActivePlayers = getAllNotActivePlayers();
                for (int i = 0; i < notActivePlayers.Count; i++)
                {
                    for (int i3 = 0; i3 < 3; i3++)
                    {
                        allPlayersTake3Cards(notActivePlayers[i]);
                    }
                }
                lastPlayer.getPlayerCards().Remove(chosenCard);
                isPlus3open = false;
            }

        }

        // if anyone agrees to use the broken 3 card
        public void continueWithout3Plus (BaseCard chosenCard)
        {
            if (isPlus3openSituation == false)       // +3 CARD is not chosed 
            {
                if (plus3BrokenPunishment == true)
                {
                    for (int indexFor3BrokenPunishment = 0; indexFor3BrokenPunishment < 3; indexFor3BrokenPunishment++)
                    {
                        takeCard();
                    }
                }
                addToRemovedCards(lastCard);
                lastPlayer.getPlayerCards().Remove(chosenCard);
                if (plus3BrokenPunishment == true)
                {
                    plus3BrokenPunishment = false;
                }
                else
                {
                    lastCard = chosenCard;
                }
            }

            else
            {
                isPlus3openSituation = false;
            }
        }
    }
}
