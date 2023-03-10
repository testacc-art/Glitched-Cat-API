using System;
using System.Collections.Generic;

namespace GlitchedCat.Domain.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; }
}