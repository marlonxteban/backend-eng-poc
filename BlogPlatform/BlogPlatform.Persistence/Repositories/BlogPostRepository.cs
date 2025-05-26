using BlogPlatform.Domain.Entities;
using BlogPlatform.Domain.Interfaces;
using BlogPlatform.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Persistence.Repositories
{
    public class BlogPostRepository: IBlogPostRepository
    {
        private readonly BlogDbContext _context;

        public BlogPostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BlogPost post)
        {
            await _context.BlogPosts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task UpdateAsync(BlogPost post)
        {
            _context.BlogPosts.Update(post);
            await _context.SaveChangesAsync();
        }

    }
}
