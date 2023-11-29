using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {

        //Place here any state you would like to keep during the app life time
        //For example, player login details...
        

        //Implememnt interface here
        public bool ShowLogin()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
            return true;
        }
        public bool ShowSignup()
        {
            Console.WriteLine("Please sign up and enter your details: ");

            TriviaDbContext db = new TriviaDbContext();
            int id = 0;
            Console.WriteLine("Enter Id: ");
            try
            {
                id = int.Parse(Console.ReadLine());
            }catch (Exception e) {  Console.WriteLine(e.Message);}
            while (db.PlayerExists(id))
            {
                try
                {
                    Console.WriteLine("Id exists. Pls enter a different id: ");
                    id = int.Parse(Console.ReadLine());
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            string email = "awdwaf@awfdawd";
            string name = "sappp";
            int score = 0;
            int rankId = 3;

            db.AddPlayer(id, email, name, score, rankId);
            if (db.PlayerExists(2))
            {
                Console.WriteLine("Exists!");
            }
            Console.ReadKey(true);
            return true;
        }

        public void ShowAddQuestion()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }

        public void ShowPendingQuestions()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowGame()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowProfile()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
