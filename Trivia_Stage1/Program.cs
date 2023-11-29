using Trivia_Stage1.Models;
using Trivia_Stage1.UI;

namespace Trivia_Stage1
{
    public class Program
    {
        public static UIMain ui;
        static void Main(string[] args)
        {
            LoginMenu loginMenu = new LoginMenu();
            TriviaScreensImp screens = new TriviaScreensImp();
            ui = new UIMain(loginMenu, screens);
            ui.ApplicationStart();
        }
    }
}