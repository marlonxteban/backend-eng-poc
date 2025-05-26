using BlogPlatform.Domain.Entities;

namespace BlogPlatform.Domain.Interfaces
{
    public interface IBlogPostService
    {
        BlogPost Create(string title, string content, string author);
        void Update(BlogPost post, string? title, string? content);
    }
}
