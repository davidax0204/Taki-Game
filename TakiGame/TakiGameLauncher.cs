using System;
using System.Collections.Generic;
using System.Text;

namespace TakiGame
{
    class TakiGameLauncher
    {
        static int Main(string[] args)
        {
            TakiGame takiGame = new TakiGame();     // start the game
            takiGame.play();
            return 0;
        }
    }
}
