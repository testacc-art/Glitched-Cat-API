using GlitchedCat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlitchedCat.Infra.Data
{
    public class BlogContext : DbContext
    {
        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost;Database=BlogDb;Trusted_Connection=True;");
            }
        }

        public DbSet<Post> Posts
        {
            get;
            set;
        }

        public DbSet<Comment> Comments
        {
            get;
            set;
        }
        public DbSet<User> Users
        {
            get;
            set;
        }
    }
}