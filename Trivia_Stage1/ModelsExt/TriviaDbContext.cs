﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Trivia_Stage1.Models
{
    public partial class TriviaDbContext : DbContext
    {
        public void ShowChangeTrackerObjects()
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();
            db.ChangeTracker.DetectChanges();
            Console.WriteLine(db.ChangeTracker.DebugView.LongView);
        }
        public void AddPlayer(int id, string email, string name, int score, int? rankId)
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
        }

        public bool PlayerExists(int id) 
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

        public bool PlayerEmailExists(string email)
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();
            foreach (Player p in db.Players)
            {
                if (p.Email.Equals(email))
                {
                    return true;
                }
            }
            return false;
        }

        public bool PlayerLoginCorrect(string email, int id)
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();
            foreach (Player p in db.Players)
            {
                if (p.PlayerId == id && p.Email.Equals(email)) 
                {
                    return true;
                }
            }
            return false;
        }

        public Player ReturnPlayerById(int id)
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();
            foreach (Player p in db.Players)
            {
                if (p.PlayerId == id)
                {
                    return p;
                }
            }
            return null;
        }

        public string GetRankByRankID(int? rankID)
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();
            foreach (Rank r in db.Ranks)
            {
                if (r.RankId == rankID)
                {
                    return r.RankName;
                }
            }
            return "";
        }

        public string GetTopicBytopicID(int? topicID)
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();
            foreach (Topic t in db.Topics)
            {
                if (t.TopicId == topicID)
                {
                    return t.TopicName;
                }
            }
            return "";
        }

        public void ChangeScoreToPlayer(int id, Action<Player> action)
        {
            Models.TriviaDbContext db = new Models.TriviaDbContext();
            foreach (Player p in db.Players)
            {
                if (p.PlayerId == id)
                {
                    action(p);
                    if (p.Score >= 100)
                    {
                        p.Score = 100;
                    }
                    if (p.Score <= 0)
                    {
                        p.Score = 0;
                    }
                    db.Entry(p).State = EntityState.Modified;
                }
            }
            ShowChangeTrackerObjects();
            db.SaveChanges();
        }
    }
}
