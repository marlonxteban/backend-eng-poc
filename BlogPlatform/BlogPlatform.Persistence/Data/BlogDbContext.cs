using Microsoft.EntityFrameworkCore;
using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Persistence.Data
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

    }
}
