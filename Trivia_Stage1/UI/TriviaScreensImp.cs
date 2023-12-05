using Microsoft.EntityFrameworkCore;
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
            player = db.ReturnPlayerById(id);
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
            player = db.ReturnPlayerById(id);
            Console.WriteLine($"Welcome to THE TRIVIA {name}");
            Console.ReadKey(true);
            return true;
        }

        public void ShowAddQuestion()
        {
            int topicId = 0;
            string text = "";
            string correct, wrong1, wrong2, wrong3;
            int statusId = 1;
            if (player.Score < 100 && player.RankId != 1)
            {
                Console.WriteLine($"Sorry, you can't add questions. You need {100-player.Score} more points.");
            }
            else
            {
                int ch = 0;
                if (player.RankId == 1)
                {
                    Console.WriteLine("Hi, dear manager!");
                    Console.WriteLine("For adding a trivia topic enter 1 (else for adding a question): ");
                    ch = int.Parse(Console.ReadLine());
                    if (ch == 1)
                    {
                        Console.WriteLine("Enter a new topic: ");
                        string newTopic = Console.ReadLine();
                        db.AddNewTopic(newTopic);
                    }
                }
                if (ch != 1)
                {
                    Console.WriteLine("*Note: adding a question will take all your points.*");
                    Console.WriteLine($"Enter topic ( {db.ReturnTopicsString()}): ");
                    try
                    {
                        topicId = int.Parse(Console.ReadLine());
                        while (topicId < 1 || topicId > db.Topics.Count())
                        {
                            topicId = int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("Enter question text: ");
                        text = Console.ReadLine();
                        Console.WriteLine("Enter the correct answer: ");
                        correct = Console.ReadLine();
                        Console.WriteLine("Enter wrong 1: ");
                        wrong1 = Console.ReadLine();
                        Console.WriteLine("Enter wrong 2: ");
                        wrong2 = Console.ReadLine();
                        Console.WriteLine("Enter wrong 3: ");
                        wrong3 = Console.ReadLine();
                        if (player.RankId == 1) { statusId = 2; }
                        db.AddQuestion(topicId, text, correct, wrong1, wrong2, wrong3, id, statusId);
                        db.ChangeScoreToPlayer(id, p => p.Score -= p.Score);
                        if (statusId == 2)
                        {
                            Console.WriteLine("Your question was approved by the system.");
                        } else
                        {
                            Console.WriteLine("Please wait for the system to approve your question.");
                        }
                    } catch (Exception e) { Console.WriteLine(e.Message); }
                }
            }
            if (player.RankId == 1 && db.NumberOfQuestionByPlayerId(player.PlayerId) >= 10)
            {
                db.ChangePlayerRank(player.PlayerId);
                Console.WriteLine("Your rank is 'Master'. From now you can approve or reject pended questions.");
            }
            Console.ReadKey(true);
        }

        public void ShowPendingQuestions()
        {
            List<Question> pQuestions = db.GetPendedQuestions();
            int choose = 0;
            if (player.RankId == 1 || player.RankId == 2)
            {
                if (pQuestions.Count != 0)
                {
                    foreach (Question q in pQuestions)
                    {
                        Console.WriteLine(db.GetQuesDetailsbyQid(q.QuestionId));
                        Console.WriteLine("Exit - 1. Approve-2. Reject - 3.");
                        choose = int.Parse(Console.ReadLine());
                        if (choose == 2)
                        {
                            db.ApproveOrRejectQuestion(true, q.QuestionId);
                            //pQuestions.Remove(q);
                        }else if (choose == 3)
                        {
                            db.ApproveOrRejectQuestion(false, q.QuestionId);
                            //pQuestions.Remove(q);
                        }
                        if (choose == 1)
                        {
                            break;
                        }
                    }
                    if (db.GetPendedQuestions().Count() == 0)
                    {
                        Console.WriteLine("There are no pended questions.");
                    }
                }else
                {
                    Console.WriteLine("There are no pended questions.");
                }
            }else
            {
                Console.WriteLine("Sorry but you are not allowed to approve or reject pending questions.");
            }
            Console.ReadKey(true);
        }
        public void ShowGame()
        {
            Console.WriteLine("Welcome to THE TRIVIA GAME");
            Console.WriteLine("--------------------------\n");
            Console.WriteLine($"score={player.Score}\n");
            int answer = 0;
            int j = 1;
            while (answer != 5)
            {
                Console.WriteLine($"Question no. {j}");
                int correct = 0;
                Random r = new Random();
                string topic = "";
                string correctAns = "";
                string wrong1 = "";
                string wrong2 = "";
                string wrong3 = "";
                string qText = "";
                int randomQid = r.Next(1, db.Questions.Count() + 1);
                while (qText.Equals(""))
                {
                    randomQid = r.Next(1, db.Questions.Count() + 1);
                    foreach (Question q in db.Questions)
                    {
                        if (q.QuestionId == randomQid && q.StatusId == 2)
                        {
                            topic = db.GetTopicBytopicID(q.TopicId);
                            correctAns = q.CorrectAnswer;
                            wrong1 = q.Wrong1;
                            wrong2 = q.Wrong2;
                            wrong3 = q.Wrong3;
                            qText = q.Text;
                        }
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
                Console.Write("\nEnter your answer (1-4, exit-5): ");
                answer = 0;
                try
                {
                    answer = int.Parse(Console.ReadLine());
                    while ((answer > 4 || answer < 1) && answer != 5)
                    {
                        Console.WriteLine("Please enter an answer between 1-4: ");
                        answer = int.Parse(Console.ReadLine());
                    }
                } catch (Exception e) { Console.WriteLine(e.Message); }
                if (answer == correct)
                {
                    player.Score = db.ChangeScoreToPlayer(id, p => p.Score += 10);
                    Console.WriteLine($"Correct! (score={player.Score})\n");
                }
                else if (answer != correct && answer != 5)
                {
                    player.Score = db.ChangeScoreToPlayer(id, p => p.Score -= 5);
                    Console.WriteLine($"Try again next time. (score={player.Score})\n");
                }
                //Console.ReadKey(true);
                j++;
                //Console.Clear();
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

            Console.WriteLine("Do you want to update your details (Email & name) y/n?");
            string answer = Console.ReadLine();
            while (answer != "yes" && answer != "no" && answer != "y" && answer != "n")
            {
                Console.WriteLine("Do you want to update your details (Email & name) y/n?");
                answer = Console.ReadLine();
            }
            if (answer == "yes" || answer == "y")
            {
                Console.WriteLine("Update email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Update name: ");
                string name = Console.ReadLine();
                db.UpdateDetails(player.PlayerId, email, name);
                Console.WriteLine("Details updated successfully!");
            }
            Console.ReadKey(true);
        }
    }
}
