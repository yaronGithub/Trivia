using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Rank
{
    [Key]
    [Column("RankID")]
    public int RankId { get; set; }

    [StringLength(50)]
    public string? RankName { get; set; }

    [InverseProperty("Rank")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
