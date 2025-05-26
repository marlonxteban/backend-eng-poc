using BlogPlatform.Domain.Entities;
using BlogPlatform.Domain.Interfaces;

namespace BlogPlatform.Domain.Services
{
    public class BlogPostService: IBlogPostService
    {
        public BlogPost Create(string title, string content, string author)
        {
            return new BlogPost(title, content, author);
        }
        public void Update(BlogPost post, string? title, string? content)
        {
            post.Update(title, content);
        }
    }
}
