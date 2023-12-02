using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Trivia_Stage1.UI
{
    class GameScreen:Screen
    {
        public GameScreen():base("Start Game")
        {


        }

        public override void Show()
        {
            base.Show();    
            Program.ui.Screens.ShowGame();
        }
    }
}
