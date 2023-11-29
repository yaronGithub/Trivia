using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.ModelsExt
{
    internal partial class TriviaDbContext : DbContext
    {
        static void ShowChangeTrackerObjects(Models.TriviaDbContext db)
        {
            db.ChangeTracker.DetectChanges();
            Console.WriteLine(db.ChangeTracker.DebugView.LongView);
        }
        public static void AddPlayer()
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();

            Console.WriteLine("Enter player id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter player email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter player name: ");
            string name = Console.ReadLine();
            int score = 0;
            int rankId = 3;

            Player p = new Player()
            {
                PlayerId = id,
                Email = email,
                PName = name,
                Score = score,
                RankId = rankId
            };

            db.Players.Add(p);
            ShowChangeTrackerObjects(db);
            db.SaveChanges();
            Console.WriteLine(p.PlayerId);
        }
    }
}
