using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Question
{
    [Key]
    [Column("QuestionID")]
    public int QuestionId { get; set; }

    [StringLength(255)]
    public string? Text { get; set; }

    [StringLength(100)]
    public string CorrectAnswer { get; set; } = null!;

    [StringLength(100)]
    public string Wrong1 { get; set; } = null!;

    [StringLength(100)]
    public string Wrong2 { get; set; } = null!;

    [StringLength(100)]
    public string Wrong3 { get; set; } = null!;

    [Column("TopicID")]
    public int? TopicId { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column("PlayerID")]
    public int? PlayerId { get; set; }

    [ForeignKey("PlayerId")]
    [InverseProperty("Questions")]
    public virtual Player? Player { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Questions")]
    public virtual Status? Status { get; set; }

    [ForeignKey("TopicId")]
    [InverseProperty("Questions")]
    public virtual Topic? Topic { get; set; }
}
