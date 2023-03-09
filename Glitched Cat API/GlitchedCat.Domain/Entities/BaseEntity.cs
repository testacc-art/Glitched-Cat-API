using System;

namespace GlitchedCat.Domain.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; set; }
    }
}