using BlogPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<List<BlogPost>> GetAllAsync();
        Task AddAsync(BlogPost post);
        Task UpdateAsync(BlogPost post);
    }
}
