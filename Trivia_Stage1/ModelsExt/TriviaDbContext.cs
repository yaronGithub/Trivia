using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Trivia_Stage1.Models
{
    public partial class TriviaDbContext : DbContext
    {
        public void ShowChangeTrackerObjects()
        {
            this.ChangeTracker.DetectChanges();
            Console.WriteLine(this.ChangeTracker.DebugView.LongView);
        }
        public void AddPlayer(int id, string email, string name, int score, int rankId)
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();

            Player p = new Player()
            {
                PlayerId = id,
                Email = email,
                PName = name,
                Score = score,
                RankId = rankId
            };

            db.Players.Add(p);
            ShowChangeTrackerObjects();
            db.SaveChanges();
            Console.WriteLine(p.PlayerId);
        }

        public bool PlayersExists(int id) 
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();
            foreach (Player p in db.Players)
            {
                if (p.PlayerId == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
