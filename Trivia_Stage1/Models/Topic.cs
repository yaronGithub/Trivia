using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Topic
{
    [Key]
    [Column("TopicID")]
    public int TopicId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string TopicName { get; set; } = null!;

    [InverseProperty("Topic")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
