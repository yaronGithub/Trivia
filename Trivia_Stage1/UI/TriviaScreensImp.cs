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
        int id;
        TriviaDbContext db = new TriviaDbContext();
        string email;
        string name;
        int score;
        int? rankId;
        Player? player;
        bool loggedIn = false;

        //Implememnt interface here
        public bool ShowLogin()
        {
            Console.WriteLine("Please log in with your id and email: ");

            Console.WriteLine("Enter Id: ");
            id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Email: ");
                email = Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            while (!db.PlayerLoginCorrect(email, id))
            {
                Console.WriteLine("Player details are incorrect. Pls enter the correct details: ");
                Console.WriteLine("Enter Id: ");
                id = 0;
                try
                {
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Email: ");
                    email = Console.ReadLine();
                } catch (Exception e) { Console.WriteLine(e.Message); }
            }
            Console.WriteLine("Logged in successfully!");
            
            loggedIn = true;
            Console.ReadKey(true);
            return true;
        }
        public bool ShowSignup()
        {
            Console.WriteLine("Please sign up and enter your details: ");

            id = 0;
            Console.WriteLine("Enter Id: ");
            try
            {
                id = int.Parse(Console.ReadLine());

                while (db.PlayerExists(id))
                {
                    try
                    {
                        Console.WriteLine("Id exists. Pls enter a different id: ");
                        id = int.Parse(Console.ReadLine());
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            } catch (Exception e) { Console.WriteLine(e.Message); }

            Console.WriteLine("Enter Email: ");
            try
            {
                email = Console.ReadLine();

                while (email.Length < 10 || !email.Contains('@') || db.PlayerEmailExists(email))
                {
                    if (email.Length < 10 || !email.Contains('@'))
                    {
                        Console.WriteLine("Please enter an email with more than 10 characters, which contains '@'.Please enter a different. ");
                        email = Console.ReadLine();
                    }
                        
                    if (db.PlayerEmailExists(email))
                    {
                        Console.WriteLine("Email aleardy exists. Please enter a different. ");
                        email = Console.ReadLine();
                    }
                }
            } catch (Exception e) { Console.WriteLine(e.Message); }

            Console.WriteLine("Enter your name: ");
            try
            {
                name = Console.ReadLine();
            }catch (Exception e) { Console.WriteLine(e.Message); }
            
            score = 0;
            rankId = 3;

            db.AddPlayer(id, email, name, score, rankId);
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
            Console.WriteLine("Welcome to the TRIVIA GAME");
            Console.WriteLine("Question no. 1");
            Console.WriteLine("---------------");

            Console.ReadKey(true);
        }
        public void ShowProfile()
        {
            player = db.ReturnPlayerById(id);
            string rank = db.GetRankByRankID(player.RankId);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Player Id: [{player.PlayerId}]");
            Console.WriteLine($"Email:     [{player.Email}]");
            Console.WriteLine($"Name:      [{player.PName}]");
            Console.WriteLine($"Score:     [{player.Score}]");
            Console.WriteLine($"Rank:      [{rank}]");
            Console.WriteLine("---------------------------------------------");
            Console.ReadKey(true);
        }
    }
}
