using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDbContext : DbContext
{
    public TriviaDbContext()
    {
    }

    public TriviaDbContext(DbContextOptions<TriviaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Trivia;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Players__4A4E74A8577BCA16");

            entity.Property(e => e.PlayerId).ValueGeneratedNever();

            entity.HasOne(d => d.Rank).WithMany(p => p.Players).HasConstraintName("FK__Players__RankID__3E52440B");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8CB4F75963");

            entity.Property(e => e.QuestionId).ValueGeneratedNever();

            entity.HasOne(d => d.Player).WithMany(p => p.Questions).HasConstraintName("FK__Questions__Playe__44FF419A");

            entity.HasOne(d => d.Status).WithMany(p => p.Questions).HasConstraintName("FK__Questions__Statu__440B1D61");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions).HasConstraintName("FK__Questions__Topic__4316F928");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.RankId).HasName("PK__Ranks__B37AFB96B93386BB");

            entity.Property(e => e.RankId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE20436AC602EB");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__Topics__022E0F7D5D66848A");

            entity.Property(e => e.TopicId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
