using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Index("Email", Name = "UQ__Players__A9D105346BC24C3F", IsUnique = true)]
public partial class Player
{
    [Key]
    [Column("PlayerID")]
    public int PlayerId { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [Column("pName")]
    [StringLength(100)]
    public string? PName { get; set; }

    public int? Score { get; set; }

    [Column("RankID")]
    public int? RankId { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [ForeignKey("RankId")]
    [InverseProperty("Players")]
    public virtual Rank? Rank { get; set; }
}
