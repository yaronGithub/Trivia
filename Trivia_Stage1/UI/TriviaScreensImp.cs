using System;
using System.Collections.Generic;
using System.IO.Pipes;
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
            Console.WriteLine($"Welcome to THE TRIVIA {name}");
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
            int correct = 0;
            Random r = new Random();
            string topic = "";
            string correctAns = "";
            string wrong1 = "";
            string wrong2 = "";
            string wrong3 = "";
            string qText = "";
            int randomQid = r.Next(1, db.Questions.Count());
            foreach (Question q in db.Questions)
            {
                if (q.QuestionId == randomQid)
                {
                    topic = db.GetTopicBytopicID(q.TopicId);
                    correctAns = q.CorrectAnswer;
                    wrong1 = q.Wrong1;
                    wrong2 = q.Wrong2;
                    wrong3 = q.Wrong3;
                    qText = q.Text;
                }
            }
            string fullQuestion = $"Topic: {topic}\n{qText}";
            int shuffleAnsAndWrongs = r.Next(1, 5);
            bool q1Asked = false;
            bool q2Asked = false;
            bool q3Asked = false;
            bool q4Asked = false;
            int i = 0;
            while (i < 4)
            {
                shuffleAnsAndWrongs = r.Next(1, 5);
                switch (shuffleAnsAndWrongs)
                {
                    case 1:
                        if (q1Asked == false)
                        {
                            fullQuestion += $"\n{i + 1}. {wrong2}";
                            i++;
                            q1Asked = true;
                        }
                        break;
                    case 2:
                        if (q2Asked == false)
                        {
                            fullQuestion += $"\n{i + 1}. {wrong3}";
                            i++;
                            q2Asked = true;
                        }
                        break;
                    case 3:
                        if (q3Asked == false)
                        {
                            fullQuestion += $"\n{i + 1}. {correctAns}";
                            correct = i + 1;
                            i++;
                            q3Asked = true;
                        }
                        break;
                    case 4:
                        if (q4Asked == false)
                        {
                            fullQuestion += $"\n{i + 1}. {wrong1}";
                            i++;
                            q4Asked = true;
                        }
                        break;
                }
            }
            Console.WriteLine(fullQuestion);
            Console.Write("\nEnter your answer (1-4): ");
            int answer = 0;
            try
            {
                answer = int.Parse(Console.ReadLine());
                while (answer > 4 || answer < 1)
                {
                    Console.WriteLine("Please enter an answer between 1-4: ");
                    answer = int.Parse(Console.ReadLine());
                }
            } catch (Exception e) { Console.WriteLine(e.Message); }
            if (answer == correct)
            {
                Console.WriteLine("Correct!");
                try { db.ChangeScoreToPlayer(id, p => p.Score += 10); } catch (Exception e) { Console.WriteLine(e.Message); };
            }
            else
            {
                Console.WriteLine("Try again next time.");
                try { db.ChangeScoreToPlayer(id, p => p.Score -= 5); } catch (Exception e) { Console.WriteLine(e.Message); };
            }
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
