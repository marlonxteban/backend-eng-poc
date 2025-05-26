using BlogPlatform.Application.DTOs;
using BlogPlatform.Domain.Interfaces;

namespace BlogPlatform.Application.UseCases
{
    public class GetBlogPostUseCases
    {
        private readonly IBlogPostRepository _repository;

        public GetBlogPostUseCases(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BlogPostDto>> GetAllAsync()
        {
            var posts = await _repository.GetAllAsync();

            return [.. posts.Select(p => new BlogPostDto
            {
                Id = p.Id,
                Title = p.Title ?? string.Empty,
                Content = p.Content ?? string.Empty,
                Author = p.Author ?? string.Empty,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })];
        }

        public async Task<BlogPostDto?> GetByIdAsync(Guid id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return null;

            return new BlogPostDto
            {
                Id = p.Id,
                Title = p.Title ?? string.Empty,
                Content = p.Content ?? string.Empty,
                Author = p.Author ?? string.Empty,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            };
        }
    }
}
